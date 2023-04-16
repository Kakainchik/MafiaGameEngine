using Net.Clients;
using Net.Contexts;
using Net.Contexts.Game;
using Net.Contexts.Night;
using Net.Contexts.Night.ActionInfo;
using System;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Threading;
using WPFApplication.Extensions;
using WPFApplication.Model;
using WPFApplication.Resources.GameStoryText;
using WPFApplication.ViewModel.Game.Screen.Night;

namespace WPFApplication.ViewModel
{
    public abstract class NightScreenState : ScreenState
    {
        private ActionStoryFacade storyFacade;

        protected bool isNightBegan;
        protected DispatcherTimer timer;
        protected TimeSpan remainedTime;
        protected RoleVisual currentRole;
        protected object _lock = new object();

        public ObservableCollection<NightPlayerState> PlayersState { get; protected internal set; }

        public TimeSpan RemainedTime
        {
            get => remainedTime;
            set
            {
                remainedTime = value;
                OnPropertyChanged(nameof(RemainedTime));
            }
        }

        public bool IsNightBegan
        {
            get => isNightBegan;
            set
            {
                isNightBegan = value;
                OnPropertyChanged(nameof(IsNightBegan));
            }
        }

        public NightScreenState(IClient client, RoleVisual role) : base(client)
        {
            storyFacade = new ActionStoryFacade(StoryParagraph);
            currentRole = role;
            timer = new DispatcherTimer(TimeSpan.FromSeconds(1),
                 DispatcherPriority.Background,
                 OnTimerTick,
                 App.Current.Dispatcher);
            PlayersState = new ObservableCollection<NightPlayerState>();

            BindingOperations.EnableCollectionSynchronization(PlayersState, _lock);
        }

        protected abstract void HandleDissalowSelection();

        public override void HandleContext(Context c)
        {
            switch(c)
            {
                case NightPlayerStateContext con:
                {
                    HandleNightPlayerState(con);
                    break;
                }
                case NightContext con when con.Step == NightStep.START_REMINDER:
                {
                    HandleStartReminder();
                    break;
                }
                case NightContext con when con.Step == NightStep.ALLOW_SELECTION:
                {
                    HandleAllowSelection();
                    break;
                }
                case ItemsContext con:
                {
                    HandleItems(con);
                    break;
                }
                case NightContext con when con.Step == NightStep.DISALLOW_SELECTION:
                {
                    IsNightBegan = false;
                    StoryClear();
                    HandleDissalowSelection();
                    break;
                }
                case NightInfoContext con:
                {
                    //Receive info about executed actions tonight
                    storyFacade.ShowStory(con);
                    break;
                }
                case NightContext con when con.Step == NightStep.END:
                {
                    HandleEnd();
                    break;
                }
                case TimerContext con when con.ToStart:
                {
                    RemainedTime = con.InitialTime;
                    timer.Start();
                    break;
                }
                default:
                    break;
            }
        }

        private void HandleNightPlayerState(NightPlayerStateContext con)
        {
            OnFadeRequested(true);

            var ap = new NightPlayerState(con.Id,
                con.Nickname,
                con.IsAlive,
                con.NColor.ConvertToColor(),
                con.IsOwn);
            lock(_lock) PlayersState.Add(ap);
        }

        protected virtual void HandleStartReminder()
        {
            StoryRun(new Run(NightResources.NightStart));
            StoryNewLine();
            StoryRun(new Run(NightResources.RoleRemind));
            StoryRun(new Run(currentRole.GetLocalizedName())
            {
                Foreground = currentRole.GetColor()
            });
            StoryNewLine();
            StoryRun(new Run(currentRole.GetLocilizedAbility())
            {
                Foreground = currentRole.GetColor()
            });
        }

        protected virtual void HandleAllowSelection()
        {
            IsNightBegan = true;
            StoryNewLine();
            StoryRun(new Run(NightResources.StartAction));
        }

        protected virtual void HandleItems(ItemsContext con)
        {
            StoryNewLine();
            StoryRun(new Run()
            {
                Text = string.Format(NightResources.RemainingBullets, con.Items),
                FontStyle = FontStyles.Italic
            });
        }

        protected virtual void HandleEnd()
        {
            StoryNewLine();
            StoryRun(new Run(NightResources.NightEnd));
        }

        private void OnTimerTick(object? sender, EventArgs e)
        {
            RemainedTime -= TimeSpan.FromSeconds(1);
            if(RemainedTime <= TimeSpan.Zero) timer.Stop();
        }
    }
}