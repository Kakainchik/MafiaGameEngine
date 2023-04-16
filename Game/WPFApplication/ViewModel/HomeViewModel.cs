using Net.Clients;
using Net.Models;
using Net.Servers.Mediators;
using System;
using System.Net;
using System.Threading;
using System.Windows.Input;
using WPFApplication.Core;
using WPFApplication.Properties;

namespace WPFApplication.ViewModel
{
    public class HomeViewModel : ChangeablePage
    {
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
            Successor?.AssertPage(page);
        }

        private void OnEnterHall(object? obj)
        {
            var nextPage = new HallViewModel()
            {
                Successor = base.Successor
            };

            HandlePageChange(nextPage);
        }
        private async void OnCreateLANGame(object? obj)
        {
            LANClient client = new LANClient(IPAddress.Loopback, SynchronizationContext.Current!);
            LobbyMediator mediator = new LobbyMediator();

            var validationResult = await mediator.AttachHost(client, Username);
            if(validationResult == ConnectValidation.ACCEPTED)
            {
                var nextPage = new LobbyHostViewModel(client, mediator)
                {
                    Successor = base.Successor
                };
                HandlePageChange(nextPage);
            }
            else
            {
                client.Disconnect();
                client.Dispose();
            }
        }

        private void OnJoinLANGame(object? obj)
        {
            var nextPage = new LANLobbyConnectionViewModel()
            {
                Successor = base.Successor
            };

            HandlePageChange(nextPage);
        }
    }
}