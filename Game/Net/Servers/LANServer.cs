using Net.Contexts;
using Net.Contexts.Connection;
using Net.Contexts.Serializers;
using Net.Models;
using Net.Servers.Mediators;
using Net.Servers.Units;
using System.Net;
using System.Net.Sockets;

namespace Net.Servers
{
    public class LANServer : ISessionCommunicator, IChatCommunicator, IDisposable
    {
        private const int IDLE_DELAY = 400;
        private const int COMMAND_PORT = 25511;
        private const int CHAT_PORT = 25512;

        private TcpListener sessionListener;
        private TcpListener chatListener;
        private Task? sessionTask;
        private Task? chatTask;
        private CancellationTokenSource sessionToken = new CancellationTokenSource();
        private CancellationTokenSource chatToken = new CancellationTokenSource();
        private IDictionary<Guid, UnitChunk> connections = new Dictionary<Guid, UnitChunk>();
        private object _lock = new();
        private bool disposedValue;

        public IMediator? SessionMediator { get; set; }
        public IMediator? ChatMediator { get; set; }
        public bool IsGameRan { get; set; }
        public bool IsNewClientsAllowed { get; set; } = true;

        public LANServer()
        {
            sessionListener = new TcpListener(IPAddress.Any, COMMAND_PORT);
            chatListener = new TcpListener(IPAddress.Any, CHAT_PORT);
        }

        public LobbyMediator InitializeFirstMediator()
        {
            var med = new LobbyMediator(this);
            SessionMediator = med;
            return med;
        }

        public void StartListenParallel()
        {
            sessionListener.Start(50);
            sessionTask = Task.Run(ListenSessionAsync, sessionToken.Token);

            chatListener.Start(50);
            chatTask = Task.Run(ListenChatAsync, chatToken.Token);
        }

        public void StopListen()
        {
            sessionToken?.Cancel();
            chatToken?.Cancel();
        }

        #region ISessionCommunicator implementation

        public void SendSessionMessage(Context message, Guid receiver)
        {
            lock(_lock)
            {
                if(connections.ContainsKey(receiver))
                {
                    connections[receiver].Session?.SendContext(message);
                }
            }
        }

        public void AcceptSystemSessionMessage(Context message)
        {
            SessionMediator?.Accept(message);
        }

        public void BroadcastSessionMessage(Context message)
        {
            lock(_lock)
            {
                byte[] data = ContextJsonSerializer.Serialize(message);
                foreach(var chunk in connections.Values)
                {
                    chunk.Session?.SendBytes(data);
                }
            }
        }

        public void BroadcastSessionMessage(Context message, Guid instead)
        {
            lock(_lock)
            {
                byte[] data = ContextJsonSerializer.Serialize(message);
                foreach(var pair in connections)
                {
                    if(pair.Key.Equals(instead)) continue;
                    pair.Value.Session?.SendBytes(data);
                }
            }
        }

        public void AbortConnection(Guid sessionId)
        {
            lock(_lock)
            {
                if(connections.Remove(sessionId, out UnitChunk? chunk))
                {
                    chunk.Dispose();

                    var msg = new DisconnectPlayerContext(sessionId);
                    BroadcastSessionMessage(msg);
                }
            }
        }

        #endregion

        #region IChatCommunicator implementation

        public void SendChatMessage(Context message, Guid receiver)
        {
            lock(_lock)
            {
                if(connections.ContainsKey(receiver))
                {
                    connections[receiver].Chat?.SendContext(message);
                }
            }
        }

        public void AcceptSystemChatMessage(Context message)
        {
            ChatMediator?.Accept(message);
        }

        public void BroadcastChatMessage(Context message)
        {
            lock(_lock)
            {
                byte[] data = ContextJsonSerializer.Serialize(message);
                foreach(var chunk in connections.Values)
                {
                    chunk.Chat?.SendBytes(data);
                }
            }
        }

        public void BroadcastChatMessage(Context message, Guid instead)
        {
            lock(_lock)
            {
                byte[] data = ContextJsonSerializer.Serialize(message);
                foreach(var pair in connections)
                {
                    if(pair.Key.Equals(instead)) continue;
                    pair.Value.Chat?.SendBytes(data);
                }
            }
        }

        public void AttachChat(ChatUnit unit)
        {
            lock(_lock)
            {
                if(connections.ContainsKey(unit.ClientId))
                {
                    connections[unit.ClientId].Chat = unit;
                }
                else
                {
                    unit.Dispose();
                }
            }
        }

        public void DetachChat(ChatUnit unit)
        {
            unit.Dispose();
            if(connections.ContainsKey(unit.ClientId))
            {
                connections[unit.ClientId].Chat = null;
            }
        }

        #endregion

        private async Task ListenSessionAsync()
        {
            using(sessionToken.Token.Register(() => sessionListener.Stop()))
            {
                while(!sessionToken.IsCancellationRequested)
                {
                    //Check before
                    if(!IsNewClientsAllowed)
                    {
                        await Task.Delay(IDLE_DELAY, sessionToken.Token);
                        continue;
                    }

                    var client = await sessionListener.AcceptTcpClientAsync(sessionToken.Token);

                    //Check after
                    if(!IsNewClientsAllowed)
                    {
                        var valid = IsGameRan ?
                            ConnectValidation.GAME_RUNNING : ConnectValidation.LOBBY_IS_FULL;
                        AuthorizationContext msg = new AuthorizationContext(valid);
                        ContextJsonSerializer.Serialize(msg, client.GetStream());
                        continue;
                    }

                    var guid = Guid.NewGuid();
                    var chunk = new UnitChunk()
                    {
                        Session = new SessionUnit(guid, client, this)
                    };
                    lock(_lock) connections[guid] = chunk;

                    _ = Task.Factory.StartNew(chunk.Session.Process,
                        sessionToken.Token,
                        TaskCreationOptions.LongRunning,
                        TaskScheduler.Current);
                }
            }
        }

        private async Task ListenChatAsync()
        {
            using(chatToken.Token.Register(() => chatListener.Stop()))
            {
                while(!chatToken.IsCancellationRequested)
                {
                    //Allow to connect even if listening paused
                    //to let existed client reconnect this unit
                    var client = await chatListener.AcceptTcpClientAsync(chatToken.Token);
                    var unit = new ChatUnit(client, this);

                    _ = Task.Factory.StartNew(unit.Process,
                        chatToken.Token,
                        TaskCreationOptions.LongRunning,
                        TaskScheduler.Current);
                }
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
                    try
                    {
                        StopListen();
                        Task.WaitAll(sessionTask, chatTask);
                    }
                    catch(AggregateException)
                    {
                        
                    }
                    finally
                    {
                        sessionTask?.Dispose();
                        chatTask?.Dispose();
                        sessionToken.Dispose();
                        chatToken.Dispose();
                    }

                    foreach(var c in connections.Values) c.Dispose();
                }

                connections.Clear();
                connections = null;
                sessionListener = null;
                chatListener = null;
                sessionTask = null;
                chatTask = null;
                sessionToken = null;
                chatToken = null;
                SessionMediator = null;
                ChatMediator = null;

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