using Net.Clients;
using Net.Contexts.Night;
using System.Windows.Input;
using WPFApplication.Core;
using WPFApplication.Model;

namespace WPFApplication.ViewModel
{
    public class TNightScreenState : NightScreenState
    {
        private NightPlayerState? pickedPrimary;
        private bool isPrimaryActive;

        public NightPlayerState? PickedPrimary
        {
            get => pickedPrimary;
            set
            {
                pickedPrimary = value;
                OnPropertyChanged(nameof(PickedPrimary));
            }
        }

        public bool IsPrimaryActive
        {
            get => isPrimaryActive;
            set
            {
                isPrimaryActive = value;
                OnPropertyChanged(nameof(IsPrimaryActive));
            }
        }

        public ICommand PlayerClickCommand { get; set; }
        public ICommand PrimaryResetCommand { get; set; }
        public ICommand PrimaryPickCommand { get; set; }

        public TNightScreenState(IClient client, RoleVisual role) : base(client, role)
        {
            PlayerClickCommand = new RelayCommand(OnPlayerClick);
            PrimaryResetCommand = new RelayCommand(OnPrimaryReset);
            PrimaryPickCommand = new RelayCommand(OnPrimaryPick);
        }

        protected override void HandleDissalowSelection()
        {
            if(pickedPrimary != null)
            {
                //Send our selected target
                var message = new SendActionContext(pickedPrimary.Details.Id);
                _ = client.SessionProvider.InformServerAsync(message);
            }
            else
            {
                //Send non executable flag
                var message = new SendActionContext();
                _ = client.SessionProvider.InformServerAsync(message);
            }
        }

        private void OnPlayerClick(object? o)
        {
            if(o is null) return;
            var state = (NightPlayerState)o;
            //Cannot pick self
            if(state.IsOwn) return;

            //Assign target only when the field is active
            if(IsPrimaryActive)
            {
                PickedPrimary = state;
                PickedPrimary.IsPicked = true;
                IsPrimaryActive = false;
            }
        }

        private void OnPrimaryReset(object? o)
        {
            IsPrimaryActive = false;
            if(PickedPrimary != null) PickedPrimary.IsPicked = false;
            PickedPrimary = null;
        }

        private void OnPrimaryPick(object? o)
        {
            IsPrimaryActive = !isPrimaryActive;
        }
    }
}