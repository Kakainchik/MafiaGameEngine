using Net.Contexts;
using Net.Contexts.Connection;
using Net.Models;
using Net.Providers;
using System.Net;
using System.Net.Sockets;

namespace Net.Clients
{
    public class LANClient : IClient
    {
        private Guid sessionId;
        private IPAddress address;
        private SynchronizationContext dispatcher;
        private LANSessionProvider sessionProvider;
        private LANChatProvider chatProvider;
        private bool disposedValue;
        private object _lock = new object();

        public IPAddress Address
        {
            get => address;
            set => address = value;
        }

        public Guid SessionId => sessionId;
        public IProvider SessionProvider => sessionProvider;
        public IProvider ChatProvider => chatProvider;

        public LANClient(IPAddress address, SynchronizationContext dispatcher)
        {
            this.address = address;
            this.dispatcher = dispatcher;
            sessionProvider = new LANSessionProvider(this, this.address, DisconnectedCallback);
            chatProvider = new LANChatProvider(this, this.address, DisconnectedCallback);
        }

        public event EventHandler<bool>? Disconnected;
        public event EventHandler<Context>? MessageIncomed;

        public async Task<ConnectValidation> ConnectAsync()
        {
            try
            {
                await sessionProvider.ConnectAsync();

                var validationContext = sessionProvider.Authorize();
                if(validationContext.Validation != ConnectValidation.ACCEPTED)
                    return validationContext.Validation;

                if(validationContext is SessionIdContext ses)
                {
                    sessionId = ses.Id;
                    sessionProvider.RunBackgroundListener();

                    await chatProvider.ConnectAsync();
                    Task chat = chatProvider.Authorize()
                        .ContinueWith(task => chatProvider.RunBackgroundListener(),
                            TaskContinuationOptions.OnlyOnRanToCompletion);

                    await Task.WhenAll(chat);
                }

                return validationContext.Validation;
            }
            catch(SocketException)
            {
                //If cannot connect to the remote server
                sessionProvider.Close();
                chatProvider.Close(suppressCallback: true);
                return ConnectValidation.CANNOT_CONNECT;
            }
            catch(Exception)
            {
                //If a task was canceled
                sessionProvider.Close();
                chatProvider.Close(suppressCallback: true);
                return ConnectValidation.CANNOT_CONNECT;
            }
        }

        public async Task<bool> RetryConnectAsync()
        {
            if(sessionId.Equals(Guid.Empty) || !sessionProvider.IsConnected)
                throw new WebException("No connection to session port",
                    WebExceptionStatus.ConnectionClosed);

            try
            {
                chatProvider.Dispose();
                chatProvider = new LANChatProvider(this, Address, DisconnectedCallback);
                await chatProvider.ConnectAsync();

                await chatProvider.Authorize();
                chatProvider.RunBackgroundListener();

                return true;
            }
            catch(SocketException)
            {
                //If cannot connect to the remote server
                chatProvider.Close(suppressCallback: false);

                return false;
            }
            catch(Exception)
            {
                //If a task was canceled
                chatProvider.Close(suppressCallback: false);

                return false;
            }
        }

        public void Disconnect()
        {
            sessionProvider?.Close();
            chatProvider?.Close(suppressCallback: true);
        }

        public void SubmitMessage(Context context)
        {
            dispatcher.Send(_ =>
            {
                MessageIncomed?.Invoke(this, context);
            }, null);
        }

        private void DisconnectedCallback(bool isMatter)
        {
            lock(_lock)
            {
                //Supress excess invocation
                if(!sessionProvider.IsConnected && !isMatter) return;
                dispatcher.Post(_ =>
                {
                    Disconnected?.Invoke(this, isMatter);
                }, null);
            }
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
                    sessionProvider.Dispose();
                    chatProvider.Dispose();
                }

                address = null;
                dispatcher = null;
                sessionProvider = null;
                chatProvider = null;
                MessageIncomed = null;
                
                disposedValue = true;
            }
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