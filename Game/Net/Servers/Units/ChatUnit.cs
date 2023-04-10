using Net.Contexts;
using Net.Contexts.Connection;
using Net.Contexts.Serializers;
using Net.Models;
using System.Net.Sockets;

namespace Net.Servers.Units
{
    public class ChatUnit : IDisposable
    {
        private bool disposedValue;
        private object _lock = new object();

        protected TcpClient client;
        protected IChatCommunicator communicator;
        protected NetworkStream? stream;

        protected internal Guid ClientId { get; protected set; }

        internal ChatUnit(TcpClient client, IChatCommunicator communicator)
        {
            this.client = client;
            this.communicator = communicator;
        }

        internal void Process()
        {
            try
            {
                stream = client.GetStream();

                if(!WaitValidation())
                {
                    Dispose(true);
                    return;
                }

                //Receiving messages from this client
                while(true)
                {
                    ChatContext? message;

                    lock(_lock)
                    {
                        message = ContextByteSerializer.Deserialize(stream) as ChatContext;
                        if(message == null) continue;
                    }


                    //We always know who sent message with his guid
                    if(message.Presenter.IsPrivate)
                    {
                        //Send private message to certain client
                        communicator.SendChatMessage(message, message.Presenter.Receiver);
                    }
                    else if(message.Presenter.IsForServer)
                    {
                        //Handle system message
                        communicator.AcceptSystemChatMessage(message);
                    }
                    else
                    {
                        //Send message to other clients
                        communicator.BroadcastChatMessage(message, ClientId);
                    }
                }
            }
            finally
            {
                //If we were disconnected
                communicator.DetachChat(this);
            }
        }

        public void SendBytes(byte[] data)
        {
            if(stream != null)
            {
                stream.Write(data, 0, data.Length);
            }
        }

        public void SendContext(Context message)
        {
            if(stream != null) ContextByteSerializer.Serialize(message, stream);
        }

        private bool WaitValidation()
        {
            Task waiter = Task.Run(() =>
            {
                var authorization = ContextByteSerializer.Deserialize(stream!) as AuthorizationContext;
                if(authorization is SessionIdContext ses)
                {
                    ClientId = ses.Id;
                    communicator.AttachChat(this);
                }
            });

            //Wait 10 seconds for session id context
            if(waiter.Wait(10_000)) return true;
            else
            {
                AuthorizationContext msg = new AuthorizationContext(ConnectValidation.CANNOT_CONNECT);
                ContextByteSerializer.Serialize(msg, stream!);
                return false;
            }
        }

        #region IDisposable implementation
#nullable disable warnings

        protected virtual void Dispose(bool disposing)
        {
            if(!disposedValue)
            {
                if(disposing)
                {
                    stream?.Dispose();
                    client.Dispose();
                }

                client = null;
                stream = null;

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