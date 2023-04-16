using GameLogic;
using Net.Contexts;
using Net.Contexts.Day;
using Net.Contexts.Lynch;
using Net.Contexts.Night;
using Net.Models;

namespace Net.Servers.Mediators
{
    public class GameRouterMediator : IMediator
    {
        private LANServer server;
        private GameHolder gameHolder;
        private bool disposedValue;

        public GameRouterMediator(LANServer server,
            IDictionary<Player, SessionPLayer> playerDataPair)
        {
            this.server = server;
            gameHolder = new GameHolder(this.server, playerDataPair);
        }

        public void Accept(Context message)
        {
            switch(message)
            {
                case SendVoteContext con:
                {
                    HandleSendVote(con);
                    break;
                }
                case SendActionContext con:
                {
                    HandleSendAction(con);
                    break;
                }
                case SendLastMessageContext con:
                {
                    HandleSendLastMessage(con);
                    break;
                }
            }
        }

        public void StartCycles()
        {
            gameHolder.RunGame();
        }

        private void HandleSendVote(SendVoteContext con)
        {
            if(con.TargetId == null)
                //Someone clicked on non-lynch object
                gameHolder.DayManager.SendVoteForNonLynch(con.Presenter.Sender);
            else if(con.TargetId.Equals(con.Presenter.Sender))
                //Someone clicked on himself - unvote
                gameHolder.DayManager.Unvote((ulong)con.TargetId!);
            else
                //Someone clicked on player
                gameHolder.DayManager.SendVoteFromTo(con.Presenter.Sender, (ulong)con.TargetId!);
        }

        private void HandleSendAction(SendActionContext con)
        {
            if(con.HasNonExFlag)
                gameHolder.NightManager.ConfirmFlag();
            //Someone picked a target at night
            else if(con is SendDActionContext dcon)
                gameHolder.NightManager.ConfirmAction(con.Presenter.Sender,
                    (ulong)dcon.PrimaryTarget!,
                    dcon.SecondaryTarget);
            else
                gameHolder.NightManager.ConfirmAction(con.Presenter.Sender,
                    (ulong)con.PrimaryTarget!);
        }

        private void HandleSendLastMessage(SendLastMessageContext con)
        {
            //Lynch player sent last message
            gameHolder.LynchManager.ConfirmLastMessage(con.LastMessage);
        }

        #region IDisposable Implementation

        protected virtual void Dispose(bool disposing)
        {
            if(!disposedValue)
            {
                if(disposing)
                {
                    server?.Dispose();
                    gameHolder?.Dispose();
                }

                // TODO: free unmanaged resources (unmanaged objects) and override finalizer
                // TODO: set large fields to null
                disposedValue = true;
            }
        }

        public void Dispose()
        {
            // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }

        #endregion
    }
}