using Net.Clients;
using Net.Contexts.Night;
using System.Threading.Tasks;
using System.Windows.Input;
using WPFApplication.Core;
using WPFApplication.Model;

namespace WPFApplication.ViewModel
{
    public class ETNightScreenState : NightScreenState
    {
        private NightPlayerState pickedPrimary;
        private NightPlayerState pickedSecondary;
        private bool isPrimaryActive;
        private bool isSecondaryActive;

        public NightPlayerState PickedPrimary
        {
            get => pickedPrimary;
            set
            {
                pickedPrimary = value;
                OnPropertyChanged(nameof(PickedPrimary));
            }
        }

        public NightPlayerState PickedSecondary
        {
            get => pickedSecondary;
            set
            {
                pickedSecondary = value;
                OnPropertyChanged(nameof(PickedSecondary));
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

        public bool IsSecondaryActive
        {
            get => isSecondaryActive;
            set
            {
                isSecondaryActive = value;
                OnPropertyChanged(nameof(IsSecondaryActive));
            }
        }

        public ICommand PlayerClickCommand { get; set; }
        public ICommand PrimaryResetCommand { get; set; }
        public ICommand PrimaryPickCommand { get; set; }
        public ICommand SecondaryResetCommand { get; set; }
        public ICommand SecondaryPickCommand { get; set; }

        public ETNightScreenState(IClient client, RoleVisual role) : base(client, role)
        {
            PlayerClickCommand = new RelayCommand(OnPlayerClick);
            PrimaryResetCommand = new RelayCommand(OnPrimaryReset);
            PrimaryPickCommand = new RelayCommand(OnPrimaryPick);
            SecondaryResetCommand = new RelayCommand(OnSecondaryReset);
            SecondaryPickCommand = new RelayCommand(OnSecondaryPick);
        }

        protected override void HandleDissalowSelection()
        {
            if(pickedPrimary != null && pickedSecondary != null)
            {
                //Send our selected target
                var message = new SendDActionContext(pickedPrimary.Details.Id,
                    pickedSecondary.Details.Id);
                _ = client.SessionProvider.InformServerAsync(message);
            }
            else
            {
                //Send non executable flag
                var message = new SendActionContext();
                _ = client.SessionProvider.InformServerAsync(message);
            }
        }

        private void OnPlayerClick(object o)
        {
            var state = (NightPlayerState)o;

            //Assign target only when the field is active
            if(IsPrimaryActive)
            {
                //Cannot pick self as primary
                if(state.IsOwn) return;

                PickedPrimary = state;
                PickedPrimary.IsPicked = true;
                IsPrimaryActive = false;
            }
            else if(IsSecondaryActive)
            {
                PickedSecondary = state;
                PickedSecondary.IsPicked = true;
                IsSecondaryActive = false;
            }
        }

        private void OnPrimaryReset(object o)
        {
            IsPrimaryActive = false;
            if(PickedPrimary != null) PickedPrimary.IsPicked = false;
            PickedPrimary = null;
        }

        private void OnPrimaryPick(object o)
        {
            IsPrimaryActive = !isPrimaryActive;
            IsSecondaryActive = false;
        }

        private void OnSecondaryReset(object o)
        {
            IsSecondaryActive = false;
            if(PickedSecondary != null) PickedSecondary.IsPicked = false;
            PickedSecondary = null;
        }

        private void OnSecondaryPick(object o)
        {
            IsPrimaryActive = false;
            IsSecondaryActive = !isSecondaryActive;
        }
    }
}