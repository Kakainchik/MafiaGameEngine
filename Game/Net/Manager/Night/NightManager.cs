using GameLogic;
using GameLogic.Cycles;
using GameLogic.Model;
using GameLogic.Roles;
using Net.Contexts.Game;
using Net.Contexts.Night;
using Net.Servers;
using System.Timers;
using Timer = System.Timers.Timer;

namespace Net.Manager.Night
{
    public class NightManager : Manager
    {
        protected readonly TimeSpan ACTION_STORY_INTERVAL = new TimeSpan(0, 0, 0, 3, 50);
        protected readonly TimeSpan SHORT_INTERVAL = new TimeSpan(0, 0, 0, 13, 50);
        protected readonly TimeSpan LONG_INTERVAL = new TimeSpan(0, 0, 1, 0, 50);

        private int counter;
        private NightDetailFacade facade;
        private Timer senderTimer;
        private Queue<BaseDetail> details;

        protected Game game;
        protected IEnumerable<Player> players;

        protected NightCycle Cycle => (NightCycle)game.CurrentCycle;

        protected override ITimerFacade StepFacade { get; }

        public NightManager(LANServer server, Game game, IEnumerable<Player> players)
            : base(server)
        {
            var steps = new TimerStruct[]
            {
                new TimerStruct(SHORT_INTERVAL.TotalMilliseconds, StartActionStep),
                new TimerStruct(LONG_INTERVAL.TotalMilliseconds, EndActionStep),
                new TimerStruct(SHORT_INTERVAL.TotalMilliseconds, EndStep),
                new TimerStruct(SHORT_INTERVAL.TotalMilliseconds, Exit)
            };
            StepFacade = new TimerFacade(steps);
            details = new Queue<BaseDetail>(0);

            this.game = game;
            this.players = players;
            facade = new NightDetailFacade(server);

            senderTimer = new Timer(ACTION_STORY_INTERVAL.TotalMilliseconds);
            senderTimer.Elapsed += Timer_Elapsed;
        }

        public void StartReminderStep()
        {
            //Send messages that night started and remind everyone about their role
            var message = new NightContext(NightStep.START_REMINDER);
            server.BroadcastSessionMessage(message);

            //Send timer
            var tmsg = new TimerContext(SHORT_INTERVAL, true);
            server.BroadcastSessionMessage(tmsg);

            StepFacade.First();
        }

        public void StartActionStep()
        {
            //Send messages that selection players for actions is allowed
            var message = new NightContext(NightStep.ALLOW_SELECTION);
            server.BroadcastSessionMessage(message);

            counter = 0;

            //Send timer
            var tmsg = new TimerContext(LONG_INTERVAL, true);
            server.BroadcastSessionMessage(tmsg);

            var vigs = players.Select(p => p.Role)
                .OfType<VigilanteRole>();
            foreach(var v in vigs)
            {
                var imsg = new ItemsContext(v.Bullets);
                server.SendSessionMessage(imsg, v.Owner.Id);
            }

            StepFacade.Next();
        }

        public void EndActionStep()
        {
            //Send message that actions ended
            var message = new NightContext(NightStep.DISALLOW_SELECTION);
            server.BroadcastSessionMessage(message);
        }

        public void ConfirmFlag()
        {
            //Comfirms non-executable flag
            if(++counter >= players.Count(p => p.IsAlive))
            {
                //Execute all actions and zip when received all flags
                var logs = Cycle.ExecuteActions();
                //Here are only actions that should be shown
                details = Cycle.ZipActionInfo(logs);

                //Start to send info
                senderTimer.Start();
            }
        }

        public void ConfirmAction(ulong exId, ulong tarId)
        {
            //Accept actions from players
            Player executor = players.First(p => p.Id.Equals(exId));
            Player primary = players.First(p => p.Id.Equals(tarId));

            //Assign command to logic
            Cycle.ConfirmAction(executor, primary);
            ConfirmFlag();
        }

        public void ConfirmAction(ulong exId, ulong primId, ulong secId)
        {
            //Accept actions from players
            Player executor = players.First(p => p.Id.Equals(exId));
            Player primary = players.First(p => p.Id.Equals(primId));
            Player secondary = players.First(p => p.Id.Equals(secId));

            //Assign command to logic
            Cycle.ConfirmAction(executor, primary, secondary);
            ConfirmFlag();
        }

        protected override void Exit()
        {
            OnHasEnded();
        }

        private void EndStep()
        {
            var message = new NightContext(NightStep.END);
            server.BroadcastSessionMessage(message);

            StepFacade.Next();
        }

        private void Timer_Elapsed(object? sender, ElapsedEventArgs e)
        {
            if(details.Count > 0)
            {
                var det = details.Dequeue();

                facade.ResolveType(det);
            }
            else
            {
                //End of the queue - stop sending
                senderTimer.Stop();
                senderTimer.Close();

                StepFacade.Next();
            }
        }

        #region IDisposable
#nullable disable warnings

        protected override void Dispose(bool disposing)
        {
            if(!disposedValue)
            {
                if(disposing)
                {
                    senderTimer.Stop();
                    senderTimer.Dispose();
                }

                facade = null;
                details = null;
                game = null;
                senderTimer = null;
                players = null;

                base.Dispose(disposing);
            }
        }

#nullable restore warnings
        #endregion
    }
}