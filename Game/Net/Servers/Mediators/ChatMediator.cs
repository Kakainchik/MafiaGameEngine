using Net.Contexts;
using Net.Contexts.Connection;

namespace Net.Servers.Mediators
{
    public class ChatMediator : IMediator
    {
        private bool disposedValue;

        public LANServer Holder { get; }

        public ChatMediator(LANServer server)
        {
            Holder = server;
        }

        public void Accept(Context context)
        {
            switch(context)
            {
                case ConnectClientIdContext con:
                {

                    break;
                }
            }
        }

        #region IDispose Immplementation

        protected virtual void Dispose(bool disposing)
        {
            if(!disposedValue)
            {
                if(disposing)
                {
                    Holder?.Dispose();
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
    }

    #endregion
}