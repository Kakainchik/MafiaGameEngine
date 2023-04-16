using Net.Clients;
using Net.Contexts;
using Net.Contexts.Connection;
using Net.Contexts.Lobby;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net;
using System.Threading;
using System.Windows.Input;
using WPFApplication.Core;
using WPFApplication.Extensions;
using WPFApplication.Model;
using WPFApplication.Properties;
using ConnectValidation = Net.Models.ConnectValidation;

namespace WPFApplication.ViewModel
{
    public class LANLobbyConnectionViewModel : ChangeablePage, INetHolder
    {
        private ConnectValidation error;
        private IClient? client;

        public string? ConnectAdress { get; set; }
        public bool IsUIEnabled { get; set; } = true;

        public ConnectValidation Error
        {
            get => error;
            set
            {
                error = value;
                OnPropertyChanged(nameof(Error));
            }
        }

        public ICommand ConnectByIPCommand { get; set; }

        public LANLobbyConnectionViewModel()
        {
            ConnectByIPCommand = new RelayCommand(OnConnectByIP);
        }

        public void AbortConnections()
        {
            //Dispose client
            client?.Disconnect();
            client?.Dispose();
        }

        public override void HandlePageChange(ChangeablePage page)
        {
            if(client is not null)
            {
                client.Disconnected -= Client_Disconnected;
                client.MessageIncomed -= Client_MessageIncomed;
            }
            Successor?.AssertPage(page);
        }

        private async void OnConnectByIP(object? o)
        {
            IsUIEnabled = false;

            //Try to connect
            IPAddress address;
            if(IPAddress.TryParse(ConnectAdress, out address!))
            {
                client = new LANClient(address, SynchronizationContext.Current!);
                client.Disconnected += Client_Disconnected;
                client.MessageIncomed += Client_MessageIncomed;

                Error = await client.ConnectAndAuthorizeAsync();

                if(Error == ConnectValidation.ACCEPTED)
                {
                    //Send own username
                    var msg = new ConnectUsernameContext(Settings.Default.LocalUsername);
                    await client.SessionProvider.InformServerAsync(msg);
                }
                else AbortConnections();
            }

            IsUIEnabled = true;
        }

        private void HandleLobbyInitialData(LobbyInitialDataContext con)
        {
            var roles = con.Roles.ToDictionary(
                key => key.Key.MapRole(),
                element => element.Value);
            var players = con.Players.Select(p =>
            {
                return new LobbyPlayer(p.Key, p.Value.Username, p.Value.IsReady);
            });

            LobbySetup setup = new LobbySetup()
            {
                MaxPlayers = con.MaxQuantityPlayers,
                Roles = roles,
                Players = new ObservableCollection<LobbyPlayer>(players)
            };

            var nextPage = new LobbyClientViewModel(client!, setup)
            {
                Successor = base.Successor
            };

            HandlePageChange(nextPage);
        }

        private async void Client_Disconnected(object? sender, bool e)
        {
            if(e) AbortConnections();
            else await client!.RetryConnectAsync();
        }

        private void Client_MessageIncomed(object? sender, Context e)
        {
            switch(e)
            {
                case LobbyInitialDataContext initial:
                {
                    HandleLobbyInitialData(initial);
                    break;
                }
            }
        }
    }
}