using Net.Clients;
using Net.Contexts;
using System.Net;
using System.Net.Sockets;

namespace Net.Providers
{
    public class LANSessionProvider : LANProvider
    {
        private const int COMMAND_PORT = 25511;

        public LANSessionProvider(IClient client, IPAddress address, Action<bool> disconnectedCallback)
            : base(client, address, disconnectedCallback)
        {

        }

        public override async Task ConnectAsync()
        {
            tcpClient = new TcpClient();
            await tcpClient.ConnectAsync(ipAddress, COMMAND_PORT);

            stream = tcpClient.GetStream();
            tokenSource = new CancellationTokenSource();
            token = tokenSource.Token;
        }

        public AuthorizationContext Authorize()
        {
            AuthorizationContext message;

            lock(_lock)
            {
                message = (AuthorizationContext)ContextByteSerializer.Deserialize(stream!);
            }

            return message;
        }

        public void RunBackgroundListener()
        {
            workerTask = Task.Factory.StartNew(StartBackgroundListen,
                token,
                TaskCreationOptions.LongRunning,
                TaskScheduler.Current);
        }

        public override void OnDisconnected()
        {
            //This provider is matter and disconnect everything
            OnDisconnected(true);
        }
    }
}