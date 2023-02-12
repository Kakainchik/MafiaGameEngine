using Net.Clients;
using Net.Contexts;
using Net.Contexts.Day;
using Net.Contexts.Game;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Media;
using System.Windows.Threading;
using WPFApplication.Extensions;
using WPFApplication.Model;
using WPFApplication.Resources.GameStoryText;

namespace WPFApplication.ViewModel
{
    public abstract class DayScreenState : ScreenState
    {
        protected DispatcherTimer timer;
        protected TimeSpan remainedTime;
        protected object _lock = new object();

        public ObservableCollection<DayPlayerState> PlayersState { get; protected internal set; }
        public DayPlayerState NonLynchInstance { get; }

        public TimeSpan RemainedTime
        {
            get => remainedTime;
            set
            {
                remainedTime = value;
                OnPropertyChanged(nameof(RemainedTime));
            }
        }

        public DayScreenState(IClient client) : base(client)
        {
            timer = new DispatcherTimer(TimeSpan.FromSeconds(1),
                DispatcherPriority.Background,
                OnTimerTick,
                App.Current.Dispatcher);
            PlayersState = new ObservableCollection<DayPlayerState>();
            NonLynchInstance = new DayPlayerState(Guid.Empty,
                "Non-Lynch",
                true,
                Colors.White);

            BindingOperations.EnableCollectionSynchronization(PlayersState, _lock);
        }

        public override void HandleContext(Context c)
        {
            switch(c)
            {
                case DayPlayerStateContext con:
                {
                    HandleDayPlayerState(con);
                    break;
                }
                case DayContext con when con.Step == DayStep.START_DAY:
                {
                    HandleStartDay(con);
                    break;
                }
                case DayContext con when con.Step == DayStep.FIRST_DAY_CASE:
                {
                    HandleFirstDayCase();
                    break;
                }
                case DayContext con when con.Step == DayStep.START_BALLOT:
                {
                    HandleStartBallot();
                    break;
                }
                case ReceiveVoteContext con when con.CurrentT == null:
                {
                    HandleUnvote(con);
                    break;
                }
                case ReceiveVoteContext con when con.CurrentT.Id.Equals(Guid.Empty):
                {
                    HandleNonLynchVote(con);
                    break;
                }
                case ReceiveVoteContext con:
                {
                    HandleVote(con);
                    break;
                }
                case DayContext con when con.Step == DayStep.END_BALLOT:
                {
                    HandleEndBallot();
                    break;
                }
                case TimerContext con when con.ToStart:
                {
                    HandleStartTimer(con);
                    break;
                }
                case TimerContext con when !con.ToStart:
                {
                    HandleStopTimer(con);
                    break;
                }
                case WarningVoteContext con:
                {
                    HandleWarningVote(con);
                    break;
                }
                case ElectionResultContext con:
                {
                    HandleElectionResult(con);
                    break;
                }
                default:
                    break;
            }
        }

        protected virtual void HandleDayPlayerState(DayPlayerStateContext con)
        {
            var ap = new DayPlayerState(con.Id,
                                        con.Nickname,
                                        con.IsAlive,
                                        con.NColor.ConvertToColor());
            lock(_lock) PlayersState.Add(ap);
        }

        protected virtual void HandleElectionResult(ElectionResultContext con)
        {
            StoryClear();
            if(con.ElectedId.Equals(Guid.Empty))
            {
                StoryRun(new Run(DayResources.LynchNotDecided));
            }
            else
            {
                //Find target
                var t = PlayersState.First(s => s.Details.Id.Equals(con.ElectedId));
                StoryRun(new Run(DayResources.LynchDecided));
                StoryRun(new Run(t.Details.Nickname)
                {
                    Foreground = new SolidColorBrush(t.Details.NColor)
                });
            }
        }

        protected virtual void HandleStartDay(DayContext con)
        {
            StoryRun(new Run()
            {
                Text = string.Format(DayResources.DayNumber, con.Number)
            });
            StoryNewLine();
            StoryRun(new Run(DayResources.MeetingStarted));
        }

        protected virtual void HandleFirstDayCase()
        {
            StoryNewLine();
            StoryRun(new Run(DayResources.FirstDayNoLynch));
        }

        protected virtual void HandleVote(ReceiveVoteContext con)
        {
            lock(_lock)
            {
                //Find target
                var t = PlayersState.First(s => s.Details.Id.Equals(con.CurrentT.Id));
                t.Vote.OwnVotes = con.CurrentT.Votes;

                //Find voter
                var v = PlayersState.First(s => s.Details.Id.Equals(con.VoterId));
                v.Vote.VoteTargetNickname = t.Details.Nickname;
                v.Vote.TColor = t.Details.NColor;

                //Find previous voter's target
                if(con.PreviousT != null)
                {
                    var p = PlayersState.FirstOrDefault(s => s.Details.Id.Equals(con.PreviousT.Id));
                    if(p == null)
                    {
                        //The target was non-lynchable object
                        NonLynchInstance.Vote.OwnVotes = con.PreviousT.Votes;
                    }
                    else
                    {
                        //Set the number of votes to previous target
                        p.Vote.OwnVotes = con.PreviousT.Votes;
                    }
                }
            }
        }

        protected virtual void HandleNonLynchVote(ReceiveVoteContext con)
        {
            //Receive vote on non-lynch
            lock(_lock)
            {
                //Find voter
                var v = PlayersState.First(s => s.Details.Id.Equals(con.VoterId));
                v.Vote.VoteTargetNickname = NonLynchInstance.Details.Nickname;
                v.Vote.TColor = NonLynchInstance.Details.NColor;

                //Handle non-lynch object
                NonLynchInstance.Vote.OwnVotes = con.CurrentT.Votes;

                //Find previous voter's target
                if(con.PreviousT != null)
                {
                    var p = PlayersState.First(s => s.Details.Id.Equals(con.PreviousT.Id));
                    //Set the number of votes to previous target
                    //It is not non-lynch object
                    p.Vote.OwnVotes = con.PreviousT.Votes;
                }
            }
        }

        protected virtual void HandleUnvote(ReceiveVoteContext con)
        {
            lock(_lock)
            {
                //Find voter
                var v = PlayersState.First(s => s.Details.Id.Equals(con.VoterId));
                v.Vote.VoteTargetNickname = null;

                //Find previous voter's target
                if(con.PreviousT != null)
                {
                    var p = PlayersState.FirstOrDefault(s => s.Details.Id.Equals(con.PreviousT.Id));
                    if(p == null)
                    {
                        //The target was non-lynchable object
                        NonLynchInstance.Vote.OwnVotes = con.PreviousT.Votes;
                    }
                    else
                    {
                        //Set the number of votes to previous target
                        p.Vote.OwnVotes = con.PreviousT.Votes;
                    }
                }
            }
        }

        protected virtual void HandleStartBallot()
        {
            //Clear panel
            StoryClear();
            StoryRun(new Run(DayResources.BallotStarted));
        }

        protected virtual void HandleEndBallot()
        {
            //Clear panel
            StoryClear();
            StoryRun(new Run(DayResources.BallotEnded));
        }

        protected virtual void HandleStartTimer(TimerContext con)
        {
            RemainedTime = con.InitialTime;
            timer.Start();
        }

        protected virtual void HandleStopTimer(TimerContext con)
        {
            timer.Stop();
            RemainedTime = con.InitialTime;

            //Clear panel
            StoryClear();
        }

        protected virtual void HandleWarningVote(WarningVoteContext con)
        {
            //Find target
            var t = PlayersState.FirstOrDefault(p => p.Details.Id.Equals(con.WarnedPlayerId));
            var waitedForLynch = t ?? NonLynchInstance;

            //Clear panel
            StoryClear();
            StoryRun(new Run(DayResources.AttentionVoting));
            StoryRun(new Run(waitedForLynch.Details.Nickname)
            {
                Foreground = new SolidColorBrush(waitedForLynch.Details.NColor)
            });
        }

        private void OnTimerTick(object sender, EventArgs e)
        {
            RemainedTime -= TimeSpan.FromSeconds(1);
            if(RemainedTime <= TimeSpan.Zero) timer.Stop();
        }
    }
}