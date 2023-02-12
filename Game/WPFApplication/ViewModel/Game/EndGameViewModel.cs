using GameLogic.Model;
using Net.Clients;
using Net.Contexts;
using Net.Contexts.Chat;
using Net.Contexts.Game;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using WPFApplication.Core;
using WPFApplication.Extensions;
using WPFApplication.Model;
using WPFApplication.Properties;
using WPFApplication.Resources.GameStoryText;

namespace WPFApplication.ViewModel
{
    public class EndGameViewModel : ChangeablePage, IFlowStory, INetUser
    {
        private readonly Brush LightGray = new SolidColorBrush(Colors.LightGray);
        private readonly Brush LightStateGray = new SolidColorBrush(Colors.LightSlateGray);

        private static string Username => Settings.Default.LocalUsername;

        private IClient client;
        private EndGamePlayerState[] playersList;
        private EndGameHistory[] history;

        private Paragraph StoryParagraph => (Paragraph)StoryLog.Blocks.FirstBlock;

        public EndGamePlayerState[] PlayersList
        {
            get => playersList;
            set
            {
                playersList = value;
                OnPropertyChanged(nameof(PlayersList));
            }
        }

        public EndGameHistory[] History
        {
            get => history;
            set
            {
                history = value;
                OnPropertyChanged(nameof(History));
            }
        }

        public FlowDocument StoryLog { get; set; }
        public ObservableCollection<ChatMessage> ChatLog { get; set; }
        public INetHolder NetHolder { get; set; }

        public ICommand PushMessageCommand { get; set; }

        public EndGameViewModel(IClient client)
        {
            this.client = client;

            StoryLog = new FlowDocument(new Paragraph());
            ChatLog = new ObservableCollection<ChatMessage>();

            PushMessageCommand = new RelayCommand(OnPushMessage);

            client.MessageIncomed += Client_MessageIncomed;
            client.Disconnected += Client_Disconnected;
        }

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

        public override void HandlePageChange(ChangeablePage page)
        {
            client.MessageIncomed -= Client_MessageIncomed;
            Successor?.AssertPage(page);
        }

        private void OnPushMessage(object o)
        {
            if(string.IsNullOrWhiteSpace((string)o)) return;

            var msg = new ChatMessage(Username, (string)o);
            var con = new MessageContext(msg.Username, msg.Message);

            _ = client.ChatProvider.SendMessageAsync(con);
            ChatLog.Insert(0, msg);
        }

        private void HandleEndGame(EndGameContext con)
        {
            StoryRun(new Run(EndResources.EndGame)
            {
                FontWeight = FontWeights.UltraBold
            });

            StoryNewLine();
            if(con.Winner.HasValue)
            {
                StoryRun(new Run(EndResources.Winner));
                StoryRun(new Run(con.Winner?.ToString())
                {
                    Foreground = con.Winner?.GetColor(),
                    FontWeight = FontWeights.Bold
                });
            }

            PrintWinnerStory(con.Winner);

            StoryNewLine();
            StoryRun(new Run(EndResources.ActionLogs)
            {
                FontWeight = FontWeights.Bold
            });

            for(int i = con.Cycles.Length - 1; i >= 0; i--)
            {
                var egh = con.Cycles[i];
                PrintWholeCycle(egh);
            }
        }

        private void PrintWinnerStory(Team? winner)
        {
            StoryNewLine();

            string text;
            switch(winner)
            {
                case Team.TOWN:
                    text = EndResources.TownWin;
                    break;
                case Team.MAFIA:
                    text = EndResources.MafiaWin;
                    break;
                case Team.CULTUS:
                    text = EndResources.CultusWin;
                    break;
                case Team.UNDEAD:
                    text = EndResources.UndeadWin;
                    break;
                case Team.SERIAL_KILLER:
                    text = EndResources.SerialKillerWin;
                    break;
                case Team.WITCH:
                    text = EndResources.WitchWin;
                    break;
                case Team.TERRORIST:
                    text = EndResources.TerroristWin;
                    break;
                default:
                    text = EndResources.NoneWin;
                    break;
            }

            StoryRun(new Run(text)
            {
                FontStyle = FontStyles.Italic
            });
        }

        private void PrintWholeCycle(EndGameHistory egh)
        {
            //Turn number
            StoryNewLine();
            StoryRun(new Run()
            {
                Text = string.Format(EndResources.TurnNumber, egh.Turn),
                Foreground = LightStateGray
            });

            //Day
            StoryNewLine();
            StoryRun(new Run(EndResources.Day)
            {
                Foreground = LightGray
            });

            //Election
            StoryNewLine();
            StoryRun(new Run()
            {
                Text = string.Format(EndResources.DayUserElected,
                    egh.DayUsernameElected ?? "Non-Lynch",
                    egh.DayVotesCount)
            });

            //Lynch
            StoryNewLine();
            if(egh.LynchLastMessage != null)
                StoryRun(new Run()
                {
                    Text = string.Format(EndResources.LynchLastMessage,
                        egh.LynchLastMessage)
                });

            //Night
            StoryNewLine();
            StoryRun(new Run(EndResources.Night)
            {
                Foreground = LightGray
            });

            for(int j = 0; j < egh.NightActions.Length; j++)
            {
                var egnh = egh.NightActions[j];
                PrintNightActions(egnh);
            }

            //Morning
            StoryNewLine();
            StoryRun(new Run()
            {
                Text = string.Format(EndResources.MorningDeath,
                egh.MorningDeathsUsername.Length),
                Foreground = LightGray
            });
            for(int j = 0; j < egh.MorningDeathsUsername.Length; j++)
            {
                StoryNewLine();
                StoryRun(new Run(egh.MorningDeathsUsername[j]));
            }
        }

        private void PrintNightActions(EndGameNightH egnh)
        {
            StoryNewLine();
            StoryRun(new Run()
            {
                Text = string.Format(EndResources.NightAction1, egnh.Executor)
            });
            StoryRun(new Run()
            {
                Text = string.Format("({0})", egnh.ERole.MapRole().GetLocalizedName()),
                Foreground = egnh.ERole.MapRole().GetColor()
            });
            StoryRun(new Run()
            {
                Text = string.Format(EndResources.NightAction2, egnh.Primary)
            });
            StoryRun(new Run()
            {
                Text = string.Format(EndResources.NightAction3, egnh.Secondary)
            });
            StoryRun(new Run()
            {
                Text = egnh.Success ? EndResources.NASuccess : EndResources.NAFailure
            });
        }

        private void Client_MessageIncomed(object sender, Context e)
        {
            switch(e)
            {
                case EndGameContext con:
                {
                    PlayersList = con.Users;
                    History = con.Cycles;

                    HandleEndGame(con);
                    break;
                }
                case MessageContext con:
                {
                    ChatLog.Insert(0, new ChatMessage(con.SenderName,
                        con.Message));
                    break;
                }
            }
        }

        private void Client_Disconnected(object sender, bool e)
        {
            if(e) NetHolder?.AbortConnections();
        }
    }
}