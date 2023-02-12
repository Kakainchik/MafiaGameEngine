using Net.Clients;
using Net.Contexts.Chat;
using Net.Contexts.Connection;
using Net.Contexts.Lobby;
using System.Collections.ObjectModel;
using System.Windows.Input;
using WPFApplication.Core;
using WPFApplication.Model;
using WPFApplication.Properties;

namespace WPFApplication.ViewModel
{
    public abstract class LobbyViewModel : ChangeablePage, INetUser
    {
        #region Variables

        protected IClient client;

        #endregion

        #region Properties

        protected string Username => Settings.Default.LocalUsername;

        public ObservableCollection<ChatMessage> ChatLog { get; set; }

        public ICommand PushMessageCommand { get; set; }
        public INetHolder NetHolder { get; set; }

        #endregion

        protected LobbyViewModel(IClient client)
        {
            this.client = client;
            ChatLog = new ObservableCollection<ChatMessage>();

            PushMessageCommand = new RelayCommand(OnPushMessage);
        }

        protected abstract void HandleConnectPlayer(ConnectPlayerContext con);
        protected abstract void HandleDisconnectPlayer(DisconnectPlayerContext con);
        protected abstract void HandleLobbyReady(LobbyReadyContext con);
        protected abstract void HandleChatMessage(MessageContext con);
        protected abstract void HandleLobbyRunGame(LobbyRunIntroContext con);

        protected virtual async void OnPushMessage(object o)
        {
            if(string.IsNullOrWhiteSpace((string)o)) return;

            var msg = new ChatMessage(Username, (string)o);
            var con = new MessageContext(Username, msg.Message);

            await client.ChatProvider.SendMessageAsync(con);
            ChatLog.Insert(0, msg);
        }
    }
}