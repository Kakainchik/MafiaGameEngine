using GameLogic.Model;
using Net.Clients;
using Net.Contexts;
using Net.Contexts.Morning;
using System;
using System.Windows;
using System.Windows.Documents;
using WPFApplication.Extensions;
using WPFApplication.Model;
using WPFApplication.Resources.GameStoryText;

namespace WPFApplication.ViewModel
{
    public class MorningScreenState : ScreenState
    {
        private readonly string[] Separator = new[] { Environment.NewLine };

        private NightPlayerState victim;
        private Visibility victimVisibility = Visibility.Hidden;

        public NightPlayerState Victim
        {
            get => victim;
            set
            {
                victim = value;
                OnPropertyChanged(nameof(Victim));
            }
        }

        public Visibility VictimVisibility
        {
            get => victimVisibility;
            set
            {
                victimVisibility = value;
                OnPropertyChanged(nameof(VictimVisibility));
            }
        }

        public MorningScreenState(IClient client) : base(client)
        {

        }

        public override void HandleContext(Context c)
        {
            switch(c)
            {
                case MorningContext con:
                {
                    ShowDayBreak(con.Deaths);
                    ShowRemainedTeam(con);

                    OnFadeRequested(false);
                    break;
                }
                case VictimContext con:
                {
                    Victim = new NightPlayerState(Guid.Empty,
                        con.Nickname,
                        false,
                        con.NColor.ConvertToColor(),
                        false);
                    ShowReasonText(con.Reason);
                    //Show last will if exists
                    ShowLastWill(con.LastWill);
                    break;
                }
                default:
                    break;
            }
        }

        private void ShowDayBreak(int deaths)
        {
            //No message
            if(deaths == 0) return;
            else if(deaths == 1)
                StoryRun(new Run(MorningResources.DayBreak1));
            else if(deaths >= 2 && deaths <= 3)
                StoryRun(new Run(MorningResources.DayBreak2_3));
            else if(deaths >= 4 && deaths <= 5)
                StoryRun(new Run(MorningResources.DayBreak4_5));
            else if(deaths >= 6 && deaths <= 7)
                StoryRun(new Run(MorningResources.DayBreak6_7));
            else if(deaths >= 8 && deaths <= 9)
                StoryRun(new Run(MorningResources.DayBreak8_9));
            else if(deaths >= 10 && deaths <= 11)
                StoryRun(new Run(MorningResources.DayBreak10_11));
            else if(deaths >= 12 && deaths <= 13)
                StoryRun(new Run(MorningResources.DayBreak12_13));
            else StoryRun(new Run(MorningResources.DayBreak14_MORE));

            //Show control if there are any deaths
            VictimVisibility = Visibility.Visible;
        }

        private void ShowRemainedTeam(MorningContext con)
        {
            StoryNewLine();
            StoryRun(new Run(MorningResources.Parity));
            StoryRun(new Run()
            {
                Text = $"{con.RTown} ",
                Foreground = Team.TOWN.GetColor()
            });
            StoryRun(new Run()
            {
                Text = $"{con.RMafia} ",
                Foreground = Team.MAFIA.GetColor()
            });
            StoryRun(new Run()
            {
                Text = $"{con.RCultus} ",
                Foreground = Team.CULTUS.GetColor()
            });
            StoryRun(new Run()
            {
                Text = $"{con.RUndead} ",
                Foreground = Team.UNDEAD.GetColor()
            });
            StoryRun(new Run()
            {
                Text = $"{con.RNeutral} ",
                Foreground = Team.SERIAL_KILLER.GetColor()
            });
        }

        private void ShowReasonText(DeathReason reason)
        {
            StoryClear();
            string text = string.Empty;

            try
            {
                string[] texts = Array.Empty<string>();

                switch(reason)
                {
                    case DeathReason.BY_MAFIA:
                    {
                        texts = MorningResources.MafiaD.Split(Separator,
                            StringSplitOptions.None);
                        break;
                    }
                    case DeathReason.BY_SERIAL_KILLER:
                    {
                        texts = MorningResources.SerialKillerD.Split(Separator,
                            StringSplitOptions.None);
                        break;
                    }
                    case DeathReason.BY_VIGILANTE:
                    {
                        texts = MorningResources.VigilanteD.Split(Separator,
                            StringSplitOptions.None);
                        break;
                    }
                    case DeathReason.BY_DRIVER:
                    {
                        texts = MorningResources.DriverD.Split(Separator,
                            StringSplitOptions.None);
                        break;
                    }
                    case DeathReason.BY_TERRORIST:
                    {
                        texts = MorningResources.TerroristD.Split(Separator,
                            StringSplitOptions.None);
                        break;
                    }
                    case DeathReason.SUICIDE:
                    {
                        texts = MorningResources.SuicideD.Split(Separator,
                            StringSplitOptions.None);
                        break;
                    }
                }

                text = texts[Random.Shared.Next(texts.Length)];
            }
            catch(IndexOutOfRangeException)
            {
                text = string.Empty;
            }
            finally
            {
                StoryRun(new Run(text));
            }
        }

        private void ShowLastWill(string will)
        {
            if(!string.IsNullOrWhiteSpace(will))
            {
                StoryNewLine();
                StoryRun(new Run(MorningResources.LastWill)
                {
                    FontStyle = FontStyles.Italic
                });
                StoryRun(new Run(will));
            }
        }
    }
}