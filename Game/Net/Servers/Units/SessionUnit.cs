using Net.Contexts;
using Net.Contexts.Connection;
using System.Net.Sockets;

namespace Net.Servers.Units
{
    public class SessionUnit : IDisposable
    {
        private bool disposedValue;
        private object _lock = new object();

        protected TcpClient client;
        protected ISessionCommunicator communicator;
        protected NetworkStream? stream;

        protected internal Guid ClientId { get; }

        internal SessionUnit(Guid id, TcpClient client, ISessionCommunicator communicator)
        {
            ClientId = id;
            this.client = client;
            this.communicator = communicator;
        }

        internal void Process()
        {
            try
            {
                stream = client.GetStream();

                //Send guid
                ContextByteSerializer.Serialize(new SessionIdContext(ClientId), stream);

                //Receiving messages from this client
                while(true)
                {
                    SessionContext? message;

                    lock(_lock)
                    {
                        message = ContextByteSerializer.Deserialize(stream) as SessionContext;
                        if(message == null) continue;
                    }

                    //We always know who sent message with his guid
                    if(message.Presenter.IsPrivate)
                    {
                        //Send private message to certain client
                        communicator.SendSessionMessage(message, message.Presenter.Receiver);
                    }
                    else if(message.Presenter.IsForServer)
                    {
                        //Handle system message
                        communicator.AcceptSystemSessionMessage(message);
                    }
                    else
                    {
                        //Send message to other clients
                        communicator.BroadcastSessionMessage(message, ClientId);
                    }
                }
            }
            finally
            {
                //If we were disconnected
                communicator.AbortConnection(ClientId);
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

        #region IDisposable implementation
#nullable disable warnings

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
                    stream?.Dispose();
                    client.Dispose();
                }

                client = null;
                stream = null;

                disposedValue = true;
            }
        }
    }
#nullable restore warnings
    #endregion
}