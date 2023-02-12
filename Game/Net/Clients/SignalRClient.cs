using Microsoft.AspNetCore.SignalR.Client;
using Net.Contexts;
using Net.Providers;

namespace Net.Clients
{
    public class SignalRClient
    {
        private const string CONNECTION_URL = "http://localhost:60479/chat";

        private HubConnection connection;

        public Guid SessionId => throw new NotImplementedException();

        public IProvider SessionProvider => throw new NotImplementedException();

        public IProvider ChatProvider => throw new NotImplementedException();

        public SignalRClient()
        {
            IHubConnectionBuilder builder = new HubConnectionBuilder();
            builder.WithAutomaticReconnect();
            builder.WithUrl(CONNECTION_URL);
            connection = builder.Build();
        }

        public event EventHandler? Connected;
        public event EventHandler<Context>? MessageIncomed;
        public event EventHandler? Disconnected;

        public void Authorize(AuthorizationContext context)
        {
            throw new NotImplementedException();
        }

        public void CloseProvider(IProvider provider)
        {
            throw new NotImplementedException();
        }

        public async Task ConnectAsync()
        {
            await connection.StartAsync();
        }

        public Task OpenProvider(IProvider provider)
        {
            throw new NotImplementedException();
        }

        public void SubmitMessage(Context context)
        {
            throw new NotImplementedException();
        }
    }
}