using Net.Clients;
using Net.Contexts;
using Net.Contexts.Chat;
using Net.Contexts.Connection;
using Net.Contexts.Lobby;
using Net.Servers.Mediators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Input;
using WPFApplication.Core;
using WPFApplication.Extensions;
using WPFApplication.Model;
using WPFApplication.Resources;

namespace WPFApplication.ViewModel
{
    public class LobbyHostViewModel : LobbyViewModel
    {
        private IntroMediator? introMediator;
        private LobbyMediator lobbyMediator;
        private bool canRun;
        private bool isReducePlayersEnabled = true;

        public HostLobbySetup HostSetup { get; set; }

        public bool IsReducePlayersEnabled
        {
            get => isReducePlayersEnabled;
            set
            {
                isReducePlayersEnabled = value;
                OnPropertyChanged(nameof(IsReducePlayersEnabled));
            }
        }

        public bool CanRun
        {
            get => canRun;
            set
            {
                canRun = value;
                OnPropertyChanged(nameof(CanRun));
            }
        }

        public ICommand DoneCommand { get; set; }
        public ICommand RoleUpdateCommand { get; set; }
        public ICommand KickCommand { get; set; }
        public ICommand PlayerNumberCommand { get; set; }

        public LobbyHostViewModel(IClient client, LobbyMediator lobbyMediator) : base(client)
        {
            HostSetup = new HostLobbySetup();
            this.lobbyMediator = lobbyMediator;

            this.lobbyMediator.MaxPlayers = HostSetup.MaxPlayers;

            KeyValuePair<ulong, Net.Models.LobbyPlayer> pair = this.lobbyMediator.ConnectedPlayers.First();
            HostSetup.Players.Add(
                new LobbyPlayer(pair.Key, pair.Value.Username, pair.Value.IsReady));

            //Show message in the chat
            ChatLog.Insert(0, new ChatMessage(ControlResources.LVMChatSystem,
                string.Format(ControlResources.LVMNewPlayer, pair.Value.Username)));

            this.client.Disconnected += Client_Disconnected;
            this.client.MessageIncomed += Client_MessageIncomed;

            DoneCommand = new RelayCommand(OnDone);
            RoleUpdateCommand = new RelayCommand(OnRoleUpdate);
            KickCommand = new RelayCommand(OnKick);
            PlayerNumberCommand = new RelayCommand(OnPlayerNumber);
        }

        public override void HandlePageChange(ChangeablePage page)
        {
            client.Disconnected -= Client_Disconnected;
            client.MessageIncomed -= Client_MessageIncomed;

            Successor?.AssertPage(page);
        }

        public override void AbortConnections()
        {
            base.AbortConnections();
            lobbyMediator?.Dispose();
            introMediator?.Dispose();
        }

        protected override void HandleConnectPlayer(ConnectPlayerContext con)
        {
            //Handle connection access
            if(lobbyMediator.IsLobbyFull)
            {
                IsReducePlayersEnabled = false;
            }

            HostSetup.Players.Add(new LobbyPlayer(con.PlayerId, con.Username));

            //Show message in the chat
            ChatLog.Insert(0, new ChatMessage(ControlResources.LVMChatSystem,
                string.Format(ControlResources.LVMNewPlayer, con.Username)));

            CheckRunPossibility();
        }

        protected override void HandleDisconnectPlayer(DisconnectPlayerContext con)
        {
            var disconnected = HostSetup.Players.Single(
                p => p.PlayerId.Equals(con.ClientId));
            HostSetup.Players.Remove(disconnected);

            //Show message in the chat
            ChatLog.Insert(0, new ChatMessage(ControlResources.LVMChatSystem,
                string.Format(ControlResources.LVMPlayerLeft, disconnected.Username)));

            //Handle connection access
            if(!lobbyMediator.IsLobbyFull)
            {
                IsReducePlayersEnabled = true;
            }
            CheckRunPossibility();
        }

        protected override void HandleLobbyReady(LobbyReadyContext con)
        {
            HostSetup.Players.Single(p => p.PlayerId.Equals(con.Presenter.Sender))
                .IsReady = con.IsReady;

            CheckRunPossibility();
        }

        protected override void HandleChatMessage(MessageContext con)
        {
            ChatLog.Insert(0, new ChatMessage(con.SenderName, con.Message));
        }

        protected override void HandleLobbyRunGame(LobbyRunIntroContext con)
        {
            if(introMediator is null)
                throw new NullReferenceException(nameof(introMediator));

            var nextPage = new IntroGameHostViewModel(client, introMediator)
            {
                Successor = base.Successor
            };
            //Next to intro page
            HandlePageChange(nextPage);
        }

        private void OnDone(object? o)
        {
            if(!CanRun) return;

            introMediator = lobbyMediator.RunIntro(HostSetup.CityName);
        }

        private void OnRoleUpdate(object? obj)
        {
            lobbyMediator.SelectedRoles = HostSetup.Roles.ToDictionary(
                key => key.Key.MapRole(),
                element => element.Value);

            CheckRunPossibility();
        }

        private void OnKick(object? obj)
        {
            if(obj is null) return;
            lobbyMediator.KickPlayer((ulong)obj);
        }

        private void OnPlayerNumber(object? obj)
        {
            lobbyMediator.MaxPlayers = HostSetup.MaxPlayers;
            //Handle connection access
            if(lobbyMediator.IsLobbyFull)
            {
                IsReducePlayersEnabled = false;
            }
            else
            {
                IsReducePlayersEnabled = true;
            }
        }

        private void CheckRunPossibility()
        {
            CanRun = lobbyMediator.CanRunGame();
        }

        private async void Client_Disconnected(object? sender, bool e)
        {
            //Own client has been disconnected
            if(e) AbortConnections();
            else await client.RetryConnectAsync();
        }

        private void Client_MessageIncomed(object? sender, Context e)
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