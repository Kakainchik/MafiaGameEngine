using GameLogic;
using GameLogic.Cycles;
using Net.Contexts.Game;
using Net.Contexts.Lynch;
using Net.Extensions;
using Net.Models;
using Net.Servers;

namespace Net.Manager.Lynch
{
    public class LynchManager : Manager
    {
        protected readonly TimeSpan SHORT_INTERVAL = new TimeSpan(0, 0, 0, 5, 50);
        protected readonly TimeSpan AVERAGE_INTERVAL = new TimeSpan(0, 0, 0, 9, 50);
        protected readonly TimeSpan LONG_INTERVAL = new TimeSpan(0, 0, 0, 13, 50);

        protected Game game;
        protected Player? elected;
        protected IDictionary<Player, SessionPLayer> players;

        protected LynchCycle Cycle => (LynchCycle)game.CurrentCycle;

        protected override ITimerFacade StepFacade { get; }

        public LynchManager(LANServer server,
            Game game,
            IDictionary<Player, SessionPLayer> players)
            : base(server)
        {
            var steps = new TimerStruct[]
            {
                new TimerStruct(SHORT_INTERVAL.TotalMilliseconds, QuestionStep),
                new TimerStruct(LONG_INTERVAL.TotalMilliseconds, LastMessageRequestStep),
                new TimerStruct(AVERAGE_INTERVAL.TotalMilliseconds, PrepareExecuteStep),
                new TimerStruct(SHORT_INTERVAL.TotalMilliseconds, ExecuteStep),
                new TimerStruct(SHORT_INTERVAL.TotalMilliseconds, ShowRoleStep),
                new TimerStruct(AVERAGE_INTERVAL.TotalMilliseconds, EndStep),
                new TimerStruct(SHORT_INTERVAL.TotalMilliseconds, Exit)
            };
            StepFacade = new TimerFacade(steps);

            this.game = game;
            this.players = players;
        }

        public void StartLynch()
        {
            elected = Cycle.GetElected();

            var msg = new LynchPlayerStateContext(
                players[elected].Nickname,
                players[elected].NColor,
                elected.Role.IntoSignature());
            server.BroadcastSessionMessage(msg, elected.Id);

            //Send info to owner
            var owmsg = new LynchPlayerStateContext(
                players[elected].Nickname,
                players[elected].NColor,
                elected.Role.IntoSignature(),
                true);
            server.SendSessionMessage(owmsg, elected.Id);

            //Also update alive data
            var smsg = new CommonPlayerStateContext(
                elected.Role.IntoSignature(),
                false);
            server.SendSessionMessage(smsg, elected.Id);

            StepFacade.First();
        }

        private void QuestionStep()
        {
            var msg = new LynchContext(LynchStep.QUESTION);
            server.BroadcastSessionMessage(msg);

            StepFacade.Next();
        }

        private void LastMessageRequestStep()
        {
            //Request last message
            var msg = new LynchContext(LynchStep.LAST_MESSAGE);
            server.SendSessionMessage(msg, elected!.Id);
        }

        public void ConfirmLastMessage(string lastMessage)
        {
            var msg = new ReceiveLastMessageContext(lastMessage);
            server.BroadcastSessionMessage(msg);

            Cycle.Lynch(lastMessage);

            StepFacade.Next();
        }

        private void PrepareExecuteStep()
        {
            var msg = new LynchContext(LynchStep.PREPARE_EXECUTE);
            server.BroadcastSessionMessage(msg);

            StepFacade.Next();
        }

        private void ExecuteStep()
        {
            var msg = new LynchContext(LynchStep.EXECUTE);
            server.BroadcastSessionMessage(msg);

            StepFacade.Next();
        }

        private void ShowRoleStep()
        {
            var msg = new LynchContext(LynchStep.SHOW_ROLE);
            server.BroadcastSessionMessage(msg);

            StepFacade.Next();
        }

        private void EndStep()
        {
            var msg = new LynchContext(LynchStep.END);
            server.BroadcastSessionMessage(msg);

            StepFacade.Next();
        }

        protected override void Exit()
        {
            OnHasEnded();
        }

        #region IDisposable
#nullable disable warnings

        protected override void Dispose(bool disposing)
        {
            if(!disposedValue)
            {
                if(disposing)
                {

                }

                game = null;
                elected = null;
                players = null;

                base.Dispose(disposing);
            }
        }

#nullable restore warnings
        #endregion
    }
}