using Net.Contexts;
using Net.Models;
using Net.Providers;
using System.Net.Http.Headers;

namespace Net.Clients
{
    public class RemoteClient : IClient
    {
        private const string API_URL = @"https://localhost:7262/api/";
        private const string HALL_URL = $"{API_URL}hall";
        private const string JSON_MIME = @"application/json";
        private const long BUFFER_SIZE = 1024 * 64;

        private string lobbyName;
        private HttpClient httpClient;
        private ulong clientId;
        private bool disposedValue;

        public ulong ClientId => clientId;
        public IProvider SessionProvider => throw new NotImplementedException();
        public IProvider ChatProvider => throw new NotImplementedException();

        public RemoteClient(string lobbyName)
        {
            this.lobbyName = lobbyName;

            httpClient = new HttpClient()
            {
                BaseAddress = new Uri(HALL_URL),
                MaxResponseContentBufferSize = BUFFER_SIZE
            };
            httpClient.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue(JSON_MIME));
        }

        public event EventHandler<bool>? Disconnected;
        public event EventHandler<Context>? MessageIncomed;

        public Task<ConnectValidation> ConnectAndAuthorizeAsync()
        {
            return Task.FromResult(ConnectValidation.ACCEPTED);
        }

        public void Disconnect()
        {
            
        }

        public Task<bool> RetryConnectAsync()
        {
            throw new NotImplementedException();
        }

        public void SubmitMessage(Context context)
        {
            throw new NotImplementedException();
        }

        #region IDisposable implementation
#nullable disable warnings

        protected virtual void Dispose(bool disposing)
        {
            if(!disposedValue)
            {
                if(disposing)
                {
                    //Dispose managed state (managed objects)
                }

                //Set large fields to null
                disposedValue = true;
            }
        }

        public void Dispose()
        {
            // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }

#nullable restore warnings
        #endregion
    }
}