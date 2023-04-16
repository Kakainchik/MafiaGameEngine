using Net.Clients;
using Net.Contexts;
using Net.Contexts.Lynch;
using System.Windows;
using System.Windows.Documents;
using System.Windows.Media;
using WPFApplication.Extensions;
using WPFApplication.Model;
using WPFApplication.Resources.GameStoryText;

namespace WPFApplication.ViewModel
{
    public class LynchScreenState : ScreenState
    {
        private NightPlayerState? electedPlayer;
        private bool isMessageBoxVisible;
        private string? lastMessageText;
        private RoleVisual lynchedRole;

        public NightPlayerState? ElectedPlayer
        {
            get => electedPlayer;
            set
            {
                electedPlayer = value;
                OnPropertyChanged(nameof(ElectedPlayer));
            }
        }

        public bool IsMessageBoxVisible
        {
            get => isMessageBoxVisible;
            set
            {
                isMessageBoxVisible = value;
                OnPropertyChanged(nameof(IsMessageBoxVisible));
            }
        }

        public string? LastMessageText
        {
            get => lastMessageText;
            set
            {
                lastMessageText = value;
                OnPropertyChanged(nameof(LastMessageText));
            }
        }

        public LynchScreenState(IClient client) : base(client)
        {

        }

        public override void HandleContext(Context c)
        {
            switch(c)
            {
                case LynchPlayerStateContext con:
                {
                    HandleLynchPlayerState(con);
                    break;
                }
                case LynchContext con when con.Step == LynchStep.QUESTION:
                {
                    HandleQuestion();
                    break;
                }
                case LynchContext con when con.Step == LynchStep.LAST_MESSAGE:
                {
                    HandleLastMessage();
                    break;
                }
                case ReceiveLastMessageContext con:
                {
                    HandleReceiveLastMessage(con);
                    break;
                }
                case LynchContext con when con.Step == LynchStep.PREPARE_EXECUTE:
                {
                    HandlePrepareExecute();
                    break;
                }
                case LynchContext con when con.Step == LynchStep.EXECUTE:
                {
                    HandleExecute();
                    break;
                }
                case LynchContext con when con.Step == LynchStep.SHOW_ROLE:
                {
                    HandleShowRole();
                    break;
                }
                case LynchContext con when con.Step == LynchStep.END:
                {
                    HandleEnd();
                    break;
                }
            }
        }

        private void HandleLynchPlayerState(LynchPlayerStateContext con)
        {
            ElectedPlayer = new NightPlayerState(0UL,
                con.Nickname,
                true,
                con.NColor.ConvertToColor(),
                con.IsOwn);

            lynchedRole = con.Role.MapRole();
            StoryRun(new Run(LynchResources.YouWereElected));
            StoryNewLine();
        }

        private void HandleQuestion()
        {
            StoryRun(new Run(LynchResources.AnyMessage));
            if(electedPlayer!.IsOwn)
            {
                IsMessageBoxVisible = true;
            }
        }

        private void HandleLastMessage()
        {
            IsMessageBoxVisible = false;

            if(!string.IsNullOrEmpty(LastMessageText))
            {
                var msg = new SendLastMessageContext(LastMessageText);
                //Run and fire
                _ = client.SessionProvider.InformServerAsync(msg);
            }
        }

        private void HandleReceiveLastMessage(ReceiveLastMessageContext con)
        {
            //Clear panel
            StoryClear();
            StoryRun(new Run(electedPlayer!.Details.Nickname)
            {
                Foreground = new SolidColorBrush(electedPlayer.Details.NColor)
            });
            StoryRun(new Run(LynchResources.LastMessage));
            StoryRun(new Run(con.LastMessage)
            {
                FontStyle = FontStyles.Italic
            });
        }

        private void HandlePrepareExecute()
        {
            StoryNewLine();
            StoryRun(new Run(LynchResources.LynchExecuted));
        }

        private void HandleExecute()
        {
            //Play sound
            ElectedPlayer!.Details.IsAlive = false;
        }

        private void HandleShowRole()
        {
            StoryClear();
            StoryRun(new Run(LynchResources.RoleWas));
            StoryRun(new Run(lynchedRole.GetLocalizedName())
            {
                Foreground = lynchedRole.GetColor()
            });
        }

        private void HandleEnd()
        {
            StoryNewLine();
            StoryRun(new Run(LynchResources.GatheringEnded));
        }
    }
}