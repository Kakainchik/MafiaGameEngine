using Net.Clients;
using Net.Contexts.Connection;
using System.Net;
using System.Net.Sockets;

namespace Net.Providers
{
    public class LANChatProvider : LANProvider
    {
        private const int CHAT_PORT = 25512;

        public LANChatProvider(IClient client, IPAddress address, Action<bool> disconnectedCallback)
            : base(client, address, disconnectedCallback)
        {

        }

        public override async Task ConnectAsync()
        {
            tcpClient = new TcpClient();
            await tcpClient.ConnectAsync(ipAddress, CHAT_PORT);

            stream = tcpClient.GetStream();
            tokenSource = new CancellationTokenSource();
            token = tokenSource.Token;
        }

        public async Task Authorize()
        {
            try
            {
                //Send existing session id
                SessionIdContext message = new(client.SessionId);

                await InformServerAsync(message);
            }
            catch(Exception)
            {
                OnDisconnected();
            }
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
            //Small delay if session provider disconnects as well
            Task.Delay(100).Wait();

            //This provider is not such matter and may reconnect
            OnDisconnected(false);
        }
    }
}