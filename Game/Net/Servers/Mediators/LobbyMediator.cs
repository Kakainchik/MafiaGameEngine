using Net.Contexts;
using Net.Contexts.Connection;
using Net.Contexts.Lobby;
using Net.Models;

namespace Net.Servers.Mediators
{
    public class LobbyMediator : IMediator
    {
        private const string HOST_NOT_PROVIDED_ERROR =
            "Host client that holds the server instance is not provided in the connection list.";
        private const string GAME_RUNNABLE_ERROR =
            "Game cannot be ran due to internal lobby conditions.";

        private LANServer server;
        private IDictionary<RoleSignature, int> selectedRoles;
        private int maxPlayers;

        private Guid HostId
        {
            get
            {
                try
                {
                    return ConnectedPlayers.Keys.First();
                }
                catch(InvalidOperationException ioe)
                {
                    throw new InvalidOperationException(HOST_NOT_PROVIDED_ERROR, ioe);
                }
            }
        }

        public IDictionary<Guid, LobbyPlayer> ConnectedPlayers { get; set; }

        public IDictionary<RoleSignature, int> SelectedRoles
        {
            get => selectedRoles;
            set
            {
                selectedRoles = value;

                var msg = new LobbyRoleContext(value);
                Task.Run(() => server.BroadcastSessionMessage(msg, HostId));
            }
        }

        public int MaxPlayers
        {
            get => maxPlayers;
            set
            {
                maxPlayers = value;
                //Server temporary close or unclose
                server.IsNewClientsAllowed = !IsLobbyFull;

                var msg = new LobbyMaxPlayerContext(value);
                Task.Run(() => server.BroadcastSessionMessage(msg, HostId));
            }
        }

        public bool IsLobbyFull => ConnectedPlayers.Count == MaxPlayers;

        public LobbyMediator(LANServer server)
        {
            this.server = server;
            ConnectedPlayers = new Dictionary<Guid, LobbyPlayer>();
            selectedRoles = new Dictionary<RoleSignature, int>();
        }

        public void KickPlayer(Guid playerId)
        {
            server.AbortConnection(playerId);
        }

        public bool CanRunGame()
        {
            int sum = SelectedRoles.Sum(p => p.Value);

            return ConnectedPlayers.All(p => p.Value.IsReady)
                //&& ConnectedPlayers.Count >= GameLogic.Game.MIN_PLAYERS
                && ConnectedPlayers.Count == sum;
        }

        public IntroMediator RunIntro(string cityName)
        {
            //Check the limit of roles
            if(!CanRunGame())
            {
                throw new InvalidOperationException(GAME_RUNNABLE_ERROR);
            }

            //Restrict connections
            server.IsNewClientsAllowed = false;
            server.IsGameRan = true;

            var message = new LobbyRunIntroContext();
            server.BroadcastSessionMessage(message);

            var mediator = new IntroMediator(server,
                ConnectedPlayers,
                SelectedRoles,
                cityName);
            server.SessionMediator = mediator;

            return mediator;
        }

        public void Accept(Context message)
        {
            switch(message)
            {
                case UsernameContext con:
                {
                    HandleUsername(con);
                    break;
                }
                case DisconnectPlayerContext con:
                {
                    HandleDisconnectPlayer(con);
                    break;
                }
                case LobbyReadyContext con:
                {
                    HandleLobbyReady(con);
                    break;
                }
            }
        }

        private void HandleUsername(UsernameContext con)
        {
            //Inform that new player has been connected
            var newPlayer = new LobbyPlayer(con.Username);
            ConnectedPlayers[con.Presenter.Sender] = newPlayer;

            var msg = new ConnectPlayerContext(con.Presenter.Sender, newPlayer.Username);
            Task.Run(() => server.BroadcastSessionMessage(msg, con.Presenter.Sender));

            var lobby = new LobbyInitialDataContext(MaxPlayers,
                ConnectedPlayers,
                SelectedRoles);
            Task.Run(() => server.SendSessionMessage(lobby, con.Presenter.Sender));

            if(IsLobbyFull)
            {
                //Server temporary close
                server.IsNewClientsAllowed = false;
            }
        }

        private void HandleDisconnectPlayer(DisconnectPlayerContext con)
        {
            bool success = ConnectedPlayers.Remove(con.SessionId);

            if(success)
            {
                Task.Run(() => server.BroadcastSessionMessage(con));

                if(!IsLobbyFull)
                {
                    //Server temporary unclose
                    server.IsNewClientsAllowed = true;
                }
            }
        }

        private void HandleLobbyReady(LobbyReadyContext con)
        {
            ConnectedPlayers[con.Presenter.Sender].IsReady = con.IsReady;
            Task.Run(() => server.BroadcastSessionMessage(con, con.Presenter.Sender));
        }
    }
}