using Net.Clients;
using Net.Contexts;
using Net.Contexts.Serializers;
using System.Net;
using System.Net.Sockets;

namespace Net.Providers
{
    public abstract class LANProvider : IProvider
    {
        protected readonly IPAddress ipAddress;

        private bool disposedValue;

        protected IClient client;
        protected TcpClient? tcpClient;
        protected NetworkStream? stream;
        protected Task? workerTask;
        protected CancellationTokenSource? tokenSource;
        protected CancellationToken token;
        protected Action<bool>? disconnectedCallback;
        protected object _lock = new object();

        public bool IsConnected => tcpClient?.Connected ?? false;

        public LANProvider(IClient client, IPAddress address, Action<bool> disconnectedCallback)
        {
            this.client = client;
            ipAddress = address;
            this.disconnectedCallback = disconnectedCallback;
        }

        #region IProvider implementation

        public abstract Task ConnectAsync();
        public abstract void OnDisconnected();

        public async Task SendMessageAsync(Context message)
        {
            if(!IsConnected)
                throw new WebException("Provider is not connected", WebExceptionStatus.ConnectFailure);

            message.Presenter.Sender = client.SessionId;
            await Task.Factory.StartNew(() => ContextJsonSerializer.Serialize(message, stream!));
        }

        public async Task SendPrivateMessageAsync(Context message, Guid receiver)
        {
            message.Presenter.IsPrivate = true;
            message.Presenter.Receiver = receiver;
            await SendMessageAsync(message);
        }

        public async Task InformServerAsync(Context message)
        {
            message.Presenter.IsForServer = true;
            await SendMessageAsync(message);
        }

        public virtual void Close(bool suppressCallback = false)
        {
            if(suppressCallback) disconnectedCallback = null;

            tokenSource?.Cancel();
            tcpClient?.Close();
        }

        #endregion

        protected virtual void StartBackgroundListen()
        {
            try
            {
                while(!token.IsCancellationRequested)
                {
                    Context message;

                    lock(_lock)
                    {
                        message = ContextJsonSerializer.Deserialize(stream!);
                    }

                    client.SubmitMessage(message);
                }
            }
            finally
            {
                OnDisconnected();
            }
        }

        protected virtual void OnDisconnected(bool isMatter)
        {
            Close();
            disconnectedCallback?.Invoke(isMatter);
        }

        #region IDispose implementation
#nullable disable warnings

        protected virtual void Dispose(bool disposing)
        {
            if(!disposedValue)
            {
                if(disposing)
                {
                    //Dispose managed state (managed objects)
                    try
                    {
                        tokenSource?.Cancel();
                        workerTask.Wait();
                    }
                    catch
                    {

                    }
                    finally
                    {
                        tokenSource?.Dispose();
                        workerTask?.Dispose();
                        tcpClient?.Dispose();
                        stream?.Dispose();
                    }
                }

                //Set large fields to null
                client = null;
                tokenSource = null;
                workerTask = null;
                tcpClient = null;
                stream = null;
                disconnectedCallback = null;

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