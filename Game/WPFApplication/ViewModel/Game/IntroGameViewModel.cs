using Net.Clients;
using Net.Contexts.Game;
using Net.Contexts.Intro;
using System.Windows;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using WPFApplication.Core;
using WPFApplication.Extensions;
using WPFApplication.Model.PlayerData;
using WPFApplication.Resources.GameStoryText;

namespace WPFApplication.ViewModel
{
    public abstract class IntroGameViewModel : ChangeablePage, IFlowStory, INetHolder
    {
        private string? city;
        private string nickname;
        private bool faded;
        private bool isNicknameBoxVisible;

        protected CommonPlayerState ownPlayer;
        protected IClient client;

        private Paragraph StoryParagraph => (Paragraph)StoryLog.Blocks.FirstBlock;

        public bool IsNicknameBoxVisible
        {
            get => isNicknameBoxVisible;
            set
            {
                isNicknameBoxVisible = value;
                OnPropertyChanged(nameof(IsNicknameBoxVisible));
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

        public FlowDocument StoryLog { get; set; }

        public ICommand EnterNicknameCommand { get; set; }

        public IntroGameViewModel(IClient client)
        {
            this.client = client;
            nickname = NicknameBank.GetRandomName(NameTemplate.GAMING);

            StoryLog = new FlowDocument(new Paragraph());

            EnterNicknameCommand = new RelayCommand(OnEnterNickname);
        }

        protected abstract void HandleIntroRunGame(IntroRunGameContext con);

        public void StoryNewLine()
        {
            StoryParagraph.Inlines.Add(new LineBreak());
        }

        public void StoryRun(Run line)
        {
            StoryParagraph.Inlines.Add(line);
        }

        public void StoryClear()
        {
            StoryParagraph.Inlines.Clear();
        }

        public virtual void AbortConnections()
        {
            client?.Disconnect();
            client?.Dispose();
        }

        protected void OnEnterNickname(object? o)
        {
            IsNicknameBoxVisible = false;

            if(string.IsNullOrWhiteSpace((string?)o)) return;
            nickname = (string)o;
        }

        protected virtual void HandleNameIn(IntroContext con)
        {
            Faded = true;

            city = con.CityName;
            StoryRun(new Run(IntroResources.NameIn)
            {
                Foreground = new SolidColorBrush(Colors.LightYellow)
            });
            StoryNewLine();
            StoryNewLine();

            IsNicknameBoxVisible = true;
        }

        protected virtual void HandleNameOut()
        {
            IsNicknameBoxVisible = false;

            //Send nickname to server
            _ = client.SessionProvider.InformServerAsync(new NicknameContext(nickname));
        }

        protected virtual void HandleIntroPlayer(IntroPlayerContext con)
        {
            Faded = false;

            //Save nickname and role in variable for late using
            ownPlayer.Nickname = con.Nickname;
            ownPlayer.NColor = con.NColor.ConvertToColor();
            ownPlayer.Role = con.Role.MapRole();

            StoryRun(new Run(IntroResources.NameOut)
            {
                Foreground = new SolidColorBrush(Colors.LightYellow)
            });
            StoryRun(new Run(con.Nickname)
            {
                Foreground = new SolidColorBrush(con.NColor.ConvertToColor()),
                FontWeight = FontWeights.Medium
            });
            StoryNewLine();
        }

        protected virtual void HandleStart()
        {
            string text = string.Format(IntroResources.Start, city);
            StoryRun(new Run(text));
            StoryNewLine();
            StoryNewLine();
        }

        protected virtual void HandleMiddle()
        {
            string text = string.Format(IntroResources.Middle, city);
            StoryRun(new Run(text));
            StoryNewLine();
            StoryNewLine();
        }

        protected virtual void HandleEnd()
        {
            StoryRun(new Run(IntroResources.End));
        }

        protected void HandleCommonPlayerState(CommonPlayerStateContext con)
        {
            //Save new info of own player state
            ownPlayer.Role = con.Role.MapRole();
            ownPlayer.IsAlive = con.IsAlive;
        }

        protected virtual void HandleTip()
        {
            StoryClear();
            StoryRun(new Run(IntroResources.RoleIs));
            StoryRun(new Run(ownPlayer.Role.GetLocalizedName())
            {
                Foreground = ownPlayer.Role.GetColor(),
                FontWeight = FontWeights.Medium
            });
            StoryNewLine();
            StoryRun(new Run()
            {
                Text = ownPlayer.Role.GetLocilizedDescription(),
                Foreground = ownPlayer.Role.GetColor()
            });
            StoryNewLine();
            StoryRun(new Run()
            {
                Text = ownPlayer.Role.GetLocilizedBelonging(),
                Foreground = ownPlayer.Role.GetColor(),
                TextDecorations = TextDecorations.Underline
            });
            StoryNewLine();
            StoryRun(new Run(IntroResources.Tip));
        }
    }
}