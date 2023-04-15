using Net.Clients;
using Net.Contexts.Connection;
using Net.Contexts.Lobby;
using Net.Models;
using Net.Servers;
using Net.Servers.Mediators;
using System;
using System.Net;
using System.Threading;
using System.Windows.Input;
using WPFApplication.Core;
using WPFApplication.Properties;

namespace WPFApplication.ViewModel
{
    public class HomeViewModel : ChangeablePage, INetHolder
    {
        private LANServer server;
        private IClient client;

        public string Username
        {
            get
            {
                var prop = Settings.Default.LocalUsername;
                if(string.IsNullOrWhiteSpace(prop))
                {
                    Settings.Default.LocalUsername = Environment.UserName;
                }
                return Settings.Default.LocalUsername;
            }
            set
            {
                Settings.Default.LocalUsername = value.Trim();
                Settings.Default.Save();
            }
        }

        public ICommand EnterHallCommand { get; set; }
        public ICommand CreateLANGameCommand { get; set; }
        public ICommand JoinLANGameCommand { get; set; }

        public HomeViewModel()
        {
            EnterHallCommand = new RelayCommand(OnEnterHall);
            CreateLANGameCommand = new RelayCommand(OnCreateLANGame);
            JoinLANGameCommand = new RelayCommand(OnJoinLANGame);
        }

        public override void HandlePageChange(ChangeablePage page)
        {
            if(client != null)
            {
                client.Disconnected -= Client_Disconnected;
            }

            Successor?.AssertPage(page);
        }

        public void AbortConnections()
        {
            //Dispose server\client
            client?.Disconnect();
            client?.Dispose();
            server?.StopListen();
            server?.Dispose();

            //Assert homepage to window
            Successor?.AssertPage(this);
        }

        private void OnEnterHall(object obj)
        {
            var nextPage = new HallViewModel()
            {
                NetHolder = this,
                Successor = base.Successor
            };

            HandlePageChange(nextPage);
        }

        private async void OnCreateLANGame(object obj)
        {
            server = new LANServer();
            server.StartListenParallel();

            client = new LANClient(IPAddress.Loopback,
                SynchronizationContext.Current!);
            client.Disconnected += Client_Disconnected;

            //Open all providers
            var validation = await client.ConnectAsync();
            if(validation == ConnectValidation.ACCEPTED)
            {
                LobbyMediator mediator = server.InitializeFirstMediator();

                //Send own username
                var msg = new UsernameContext(Username);
                await client.SessionProvider.InformServerAsync(msg);

                //Send ready flag as host
                var rmsg = new LobbyReadyContext(true);
                await client.SessionProvider.InformServerAsync(rmsg);

                var nextPage = new LobbyHostViewModel(client, mediator)
                {
                    NetHolder = this,
                    Successor = base.Successor
                };
                HandlePageChange(nextPage);
            }
            else AbortConnections();
        }

        private void OnJoinLANGame(object obj)
        {
            var nextPage = new LANLobbyConnectionViewModel()
            {
                NetHolder = this,
                Successor = base.Successor
            };

            HandlePageChange(nextPage);
        }

        private async void Client_Disconnected(object? sender, bool e)
        {
            if(e) AbortConnections();
            else await client.RetryConnectAsync();
            //TODO: Show some message if could not reconnect chatProvider
        }
    }
}