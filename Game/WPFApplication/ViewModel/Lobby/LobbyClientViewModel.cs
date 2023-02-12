using Net.Clients;
using Net.Contexts;
using Net.Contexts.Chat;
using Net.Contexts.Connection;
using Net.Contexts.Lobby;
using System.Linq;
using System.Windows.Input;
using WPFApplication.Core;
using WPFApplication.Extensions;
using WPFApplication.Model;
using WPFApplication.Resources;

namespace WPFApplication.ViewModel
{
    public class LobbyClientViewModel : LobbyViewModel
    {
        private bool isReady = false;

        public bool IsReady
        {
            get => isReady;
            set
            {
                isReady = value;
                OnPropertyChanged(nameof(IsReady));
            }
        }

        public LobbySetup HostSetup { get; set; }

        public ICommand ReadyCommand { get; set; }

        public LobbyClientViewModel(IClient client, LobbySetup initialSetup) : base(client)
        {
            this.client = client;
            HostSetup = initialSetup;

            this.client.Disconnected += Client_Disconnected;
            this.client.MessageIncomed += Client_MessageIncomed;

            ReadyCommand = new RelayCommand(OnReady);
        }

        public override void HandlePageChange(ChangeablePage page)
        {
            client.Disconnected -= Client_Disconnected;
            client.MessageIncomed -= Client_MessageIncomed;

            Successor?.AssertPage(page);
        }

        protected override void HandleConnectPlayer(ConnectPlayerContext con)
        {
            HostSetup.Players.Add(new LobbyPlayer(con.PlayerId, con.Username));

            //Show message in the chat
            ChatLog.Insert(0, new ChatMessage(ControlResources.LVMChatSystem,
                string.Format(ControlResources.LVMNewPlayer, con.Username)));
        }

        protected override void HandleDisconnectPlayer(DisconnectPlayerContext con)
        {
            var disconnected = HostSetup.Players.Single(
                p => p.PlayerId.Equals(con.SessionId));
            HostSetup.Players.Remove(disconnected);

            //Show message in the chat
            ChatLog.Insert(0, new ChatMessage(ControlResources.LVMChatSystem,
                string.Format(ControlResources.LVMPlayerLeft, disconnected.Username)));
        }

        protected override void HandleLobbyReady(LobbyReadyContext con)
        {
            HostSetup.Players.Single(p => p.PlayerId.Equals(con.Presenter.Sender))
                .IsReady = con.IsReady;
        }

        protected override void HandleChatMessage(MessageContext con)
        {
            ChatLog.Insert(0, new ChatMessage(con.SenderName, con.Message));
        }

        protected override void HandleLobbyRunGame(LobbyRunIntroContext con)
        {
            var nextPage = new IntroGameClientViewModel(client)
            {
                NetHolder = base.NetHolder,
                Successor = base.Successor
            };
            //Next to intro page
            HandlePageChange(nextPage);
        }

        private void HandleLobbyMaxPlayer(LobbyMaxPlayerContext con)
        {
            HostSetup.MaxPlayers = con.Quantity;
        }

        private void HandleLobbyRole(LobbyRoleContext con)
        {
            HostSetup.Roles = con.Roles.ToDictionary(
                key => key.Key.MapRole(),
                element => element.Value);
        }

        private async void OnReady(object o)
        {
            var msg = new LobbyReadyContext(isReady);
            await client.SessionProvider.InformServerAsync(msg);

            HostSetup.Players.Single(
                p => p.PlayerId.Equals(client.SessionId)).IsReady = isReady;
        }

        private async void Client_Disconnected(object sender, bool e)
        {
            if(e) NetHolder?.AbortConnections();
            else await client.RetryConnectAsync();
        }

        private void Client_MessageIncomed(object sender, Context e)
        {
            switch(e)
            {
                case ConnectPlayerContext con:
                {
                    HandleConnectPlayer(con);
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
                case LobbyMaxPlayerContext con:
                {
                    HandleLobbyMaxPlayer(con);
                    break;
                }
                case LobbyRoleContext con:
                {
                    HandleLobbyRole(con);
                    break;
                }
                case MessageContext con:
                {
                    HandleChatMessage(con);
                    break;
                }
                case LobbyRunIntroContext con:
                {
                    HandleLobbyRunGame(con);
                    break;
                }
                default:
                    break;
            }
        }
    }
}