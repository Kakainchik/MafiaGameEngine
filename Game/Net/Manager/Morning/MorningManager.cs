using GameLogic;
using GameLogic.Cycles;
using GameLogic.Model;
using Net.Contexts.Morning;
using Net.Extensions;
using Net.Models;
using Net.Servers;
using System.Timers;
using Timer = System.Timers.Timer;

namespace Net.Manager.Morning
{
    public class MorningManager : Manager
    {
        protected readonly TimeSpan AVERAGE_INTERVAL = new TimeSpan(0, 0, 0, 5, 50);

        protected Game game;
        protected IDictionary<Player, SessionPLayer> players;
        protected Timer senderTimer;
        protected Queue<DeathDetail> death;

        protected MorningCycle Cycle => (MorningCycle)game.CurrentCycle;

        protected override ITimerFacade StepFacade { get; }

        public MorningManager(LANServer server,
            Game game,
            IDictionary<Player, SessionPLayer> players)
            : base(server)
        {
            var steps = new TimerStruct[]
            {
                new TimerStruct(AVERAGE_INTERVAL.TotalMilliseconds, Exit)
            };
            StepFacade = new TimerFacade(steps);
            death = new Queue<DeathDetail>(0);

            this.game = game;
            this.players = players;
            senderTimer = new Timer(AVERAGE_INTERVAL.TotalMilliseconds);
            senderTimer.Elapsed += SenderTimer_Elapsed;
        }

        public void StartMorning()
        {
            //Get all death at previous night
            death = new Queue<DeathDetail>(Cycle.NoteDeaths());

            var msg = new MorningContext((byte)death.Count,
                (byte)Cycle.TownPlayerNumber,
                (byte)Cycle.MafiaPlayerNumber,
                (byte)Cycle.CultusPlayerNumber,
                (byte)Cycle.UndeadPlayerNumber,
                (byte)Cycle.NeutralPlayerNumber);
            server.BroadcastSessionMessage(msg);

            //Start to send info
            senderTimer.Start();
        }

        protected override void Exit()
        {
            OnHasEnded();
        }

        private void NoteVictim(DeathDetail death)
        {
            var msg = new VictimContext(players[death.Victim].Nickname,
                players[death.Victim].NColor,
                death.Victim.Role.IntoSignature(),
                death.Reason,
                death.Victim.LastWill);
            server.BroadcastSessionMessage(msg);
        }

        private void SenderTimer_Elapsed(object? sender, ElapsedEventArgs e)
        {
            if(death.Count > 0)
            {
                var dea = death.Dequeue();

                NoteVictim(dea);
            }
            else
            {
                //End of the queue - stop sending
                senderTimer.Stop();
                senderTimer.Close();

                StepFacade.Exit();
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

                game = null;
                players = null;
                senderTimer = null;
                death = null;

                base.Dispose(disposing);
            }
        }

#nullable restore warnings
        #endregion
    }
}