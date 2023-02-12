using GameLogic.Attributes;
using Net.Clients;
using Net.Contexts.Chat;
using Net.Contexts.Game;
using Net.Models;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using WPFApplication.Core;
using WPFApplication.Extensions;
using WPFApplication.Model;
using WPFApplication.Model.PlayerData;

namespace WPFApplication.ViewModel
{
    public abstract class RunningGameViewModel : ChangeablePage, INetUser
    {
        #region Variables

        private bool isMainChatEnabled;
        private bool isDeadChatEnabled;
        private bool faded;
        private ScreenType currentScreen;
        private Visibility mainChatVisibility = Visibility.Hidden;
        private Visibility deadChatVisibility = Visibility.Hidden;

        protected IClient client;
        protected Screen mainScreen;
        protected CommonPlayerState ownPlayer;
        protected IScreenFactory dayFactory;
        protected IScreenFactory lynchFactory;
        protected IScreenFactory nightFactory;
        protected IScreenFactory morningFactory;

        #endregion

        #region Properties

        public bool IsMainChatEnabled
        {
            get => isMainChatEnabled;
            set
            {
                isMainChatEnabled = value;
                OnPropertyChanged(nameof(IsMainChatEnabled));
            }
        }

        public bool IsDeadChatEnabled
        {
            get => isDeadChatEnabled;
            set
            {
                isDeadChatEnabled = value;
                OnPropertyChanged(nameof(IsDeadChatEnabled));
            }
        }

        public bool Faded
        {
            get => faded;
            set
            {
                faded = value;
                OnPropertyChanged(nameof(Faded));
            }
        }

        public Visibility MainChatVisibility
        {
            get => mainChatVisibility;
            set
            {
                mainChatVisibility = value;
                OnPropertyChanged(nameof(MainChatVisibility));
            }
        }

        public Visibility DeadChatVisibility
        {
            get => deadChatVisibility;
            set
            {
                deadChatVisibility = value;
                OnPropertyChanged(nameof(DeadChatVisibility));
            }
        }

        public Screen MainScreen
        {
            get => mainScreen;
            set
            {
                mainScreen = value;
                OnPropertyChanged(nameof(MainScreen));
            }
        }

        public ObservableCollection<ColoredChatMessage> MainChatLog { get; set; }
        public ObservableCollection<ColoredChatMessage> DeadChatLog { get; set; }

        public ICommand PushMainMessageCommand { get; set; }
        public ICommand PushDeadMessageCommand { get; set; }
        public INetHolder NetHolder { get; set; }

        #endregion

        protected RunningGameViewModel(IClient client, CommonPlayerState ownPlayer)
        {
            this.client = client;
            this.ownPlayer = ownPlayer;
            MainChatLog = new ObservableCollection<ColoredChatMessage>();
            DeadChatLog = new ObservableCollection<ColoredChatMessage>();
            MainScreen = new Screen(null);
            dayFactory = new DayScreenFactory(this.client);
            lynchFactory = new LynchScreenFactory(this.client);
            nightFactory = new NightScreenFactory(this.client);
            morningFactory = new MorningScreenFactory(this.client);

            PushMainMessageCommand = new RelayCommand(OnPushMainMessage);
            PushDeadMessageCommand = new RelayCommand(OnPushDeadMessage);
        }

        protected void ResolveScreen(ScreenContext con)
        {
            currentScreen = con.Screen;
            if(mainScreen.State != null)
                mainScreen.State.FadeRequested -= State_FadeRequested;
            switch(con.Screen)
            {
                case ScreenType.MORNING:
                {
                    mainScreen.State = morningFactory.Create(ownPlayer.Role,
                        ownPlayer.IsAlive);

                    MainChatVisibility = Visibility.Collapsed;
                    DeadChatVisibility = Visibility.Collapsed;
                    break;
                }
                case ScreenType.DAY:
                {
                    mainScreen.State = dayFactory.Create(ownPlayer.Role,
                        ownPlayer.IsAlive);
                    ActivateDayChats();
                    break;
                }
                case ScreenType.LYNCH:
                {
                    mainScreen.State = lynchFactory.Create(ownPlayer.Role,
                        ownPlayer.IsAlive);
                    MainChatVisibility = Visibility.Collapsed;
                    DeadChatVisibility = Visibility.Collapsed;
                    break;
                }
                case ScreenType.NIGHT:
                {
                    mainScreen.State = nightFactory.Create(ownPlayer.Role,
                        ownPlayer.IsAlive);
                    ActivateNightChats();
                    break;
                }
                case ScreenType.WIN:
                {
                    var nextPage = new EndGameViewModel(client)
                    {
                        NetHolder = this.NetHolder,
                        Successor = base.Successor
                    };

                    HandlePageChange(nextPage);
                    break;
                }
            }

            mainScreen.State.FadeRequested += State_FadeRequested;
        }

        protected void ActivateDayChats()
        {
            MainChatLog.Clear();
            DeadChatLog.Clear();

            //During a day the main chat is always visible
            MainChatVisibility = Visibility.Visible;

            if(!ownPlayer.IsAlive)
            {
                //Dead player sees both chats
                DeadChatVisibility = Visibility.Visible;
                IsMainChatEnabled = false;
                IsDeadChatEnabled = true;
                return;
            }

            IsMainChatEnabled = true;

            var scopes = ownPlayer.Role.GetChatScopes();
            if(scopes.Any(s => s.scope == ChatScope.DEAD))
            {
                //Alive player with this ability can see the chat but write
                DeadChatVisibility = Visibility.Visible;
                IsDeadChatEnabled = false;
            }
            else
            {
                //The rest roles have only day chat
                DeadChatVisibility = Visibility.Collapsed;
            }
        }

        protected void ActivateNightChats()
        {
            MainChatLog.Clear();
            DeadChatLog.Clear();

            if(!ownPlayer.IsAlive)
            {
                //Dead player sees only secondary chat
                MainChatVisibility = Visibility.Collapsed;
                DeadChatVisibility = Visibility.Visible;
                IsDeadChatEnabled = true;
                return;
            }

            MainChatVisibility = Visibility.Collapsed;
            DeadChatVisibility = Visibility.Collapsed;

            var scopes = ownPlayer.Role.GetChatScopes();
            foreach(var s in scopes)
            {
                if(s.scope == ChatScope.DEAD)
                {
                    DeadChatVisibility = Visibility.Visible;
                    IsDeadChatEnabled = s.canWrite;
                }
                else
                {
                    MainChatVisibility = Visibility.Visible;
                    IsMainChatEnabled = s.canWrite;
                }
            }
        }

        protected void TransmitScopedMessage(ScopedMessageContext con)
        {
            var message = new ColoredChatMessage(con.NColor.ConvertToColor(),
                    con.SenderName,
                    con.Message);
            if(con.Scope == ChatScope.GENERAL_ALIVE)
            {
                MainChatLog.Insert(0, message);
            }
            else if(con.Scope == ChatScope.DEAD)
            {
                DeadChatLog.Insert(0, message);
            }
            else if(ownPlayer.Role.GetChatScopes().Any(s => s.scope == con.Scope))
            {
                MainChatLog.Insert(0, message);
            }
        }

        private async void OnPushMainMessage(object o)
        {
            if(string.IsNullOrWhiteSpace((string)o)) return;

            var msg = new ColoredChatMessage(ownPlayer.NColor, ownPlayer.Nickname, (string)o);

            var con = new ScopedMessageContext(msg.Username,
                msg.Message,
                currentScreen == ScreenType.NIGHT ? ownPlayer.MainInputChatScope : ChatScope.GENERAL_ALIVE,
                ownPlayer.NColor.ConvertToBytes());

            await client.ChatProvider.SendMessageAsync(con);
            MainChatLog.Insert(0, msg);
        }

        private async void OnPushDeadMessage(object o)
        {
            if(string.IsNullOrWhiteSpace((string)o)) return;

            var msg = new ColoredChatMessage(ownPlayer.NColor, ownPlayer.Nickname, (string)o);
            var con = new ScopedMessageContext(msg.Username,
                msg.Message,
                ChatScope.DEAD,
                ownPlayer.NColor.ConvertToBytes());

            await client.ChatProvider.SendMessageAsync(con);
            DeadChatLog.Insert(0, msg);
        }

        private void State_FadeRequested(object sender, bool e)
        {
            Faded = e;
        }
    }
}