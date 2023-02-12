namespace Net.Servers.Units
{
    internal class UnitChunk : IDisposable
    {
        private bool disposedValue;

        internal SessionUnit? Session { get; set; }
        internal ChatUnit? Chat { get; set; }

        internal UnitChunk()
        {

        }

        public void Dispose()
        {
            // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if(!disposedValue)
            {
                if(disposing)
                {
                    Session?.Dispose();
                    Chat?.Dispose();
                }

                disposedValue = true;
            }
        }
    }
}