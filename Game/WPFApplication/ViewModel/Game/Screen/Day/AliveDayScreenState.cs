using WPFApplication.Core;
using WPFApplication.Model;
using System.Windows.Input;
using Net.Clients;
using Net.Contexts.Day;
using System;

namespace WPFApplication.ViewModel
{
    public class AliveDayScreenState : DayScreenState
    {
        private bool isBallotBegan;

        public bool IsBallotBegan
        {
            get => isBallotBegan;
            set
            {
                isBallotBegan = value;
                OnPropertyChanged(nameof(IsBallotBegan));
            }
        }

        public ICommand VoteCommand { get; set; }
        public ICommand NonLynchCommand { get; set; }

        public AliveDayScreenState(IClient client) : base(client)
        {
            //Commands
            VoteCommand = new RelayCommand(OnVote);
            NonLynchCommand = new RelayCommand(OnNonLynch);
        }

        protected override void HandleStartBallot()
        {
            IsBallotBegan = true;
            base.HandleStartBallot();
        }

        protected override void HandleEndBallot()
        {
            IsBallotBegan = false;
            base.HandleEndBallot();
        }

        private void OnVote(object o)
        {
            var state = (DayPlayerState)o;

            var message = new SendVoteContext(state.Details.Id);
            _ = client.SessionProvider.InformServerAsync(message);
        }

        private void OnNonLynch(object o)
        {
            //Send null username that determines non-lynch object
            var message = new SendVoteContext(Guid.Empty);
            _ = client.SessionProvider.InformServerAsync(message);
        }
    }
}