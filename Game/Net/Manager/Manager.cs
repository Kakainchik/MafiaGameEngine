using Net.Servers;

namespace Net.Manager
{
    public abstract class Manager : IDisposable
    {
        protected LANServer server;
        protected bool disposedValue;

        protected abstract ITimerFacade StepFacade { get; }

        public Manager(LANServer server)
        {
            this.server = server;
        }

        public event EventHandler? HasEnded;

        protected abstract void Exit();

        protected virtual void OnHasEnded()
        {
            HasEnded?.Invoke(this, EventArgs.Empty);
        }

        #region IDisposable
#nullable disable warnings

        protected virtual void Dispose(bool disposing)
        {
            if(!disposedValue)
            {
                if(disposing)
                {
                    StepFacade?.Dispose();
                }

                server = null;
                HasEnded = null;

                disposedValue = true;
            }
        }

        ~Manager()
        {
            Dispose(false);
        }

        public void Dispose()
        {
            //Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }

#nullable restore warnings
        #endregion
    }
}