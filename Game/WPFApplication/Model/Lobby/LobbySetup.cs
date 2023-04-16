using WPFApplication.Core;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace WPFApplication.Model
{
    public class LobbySetup : ObservableObject
    {
        private int maxPlayers;
        private IDictionary<RoleVisual, int> roles;
        private ObservableCollection<LobbyPlayer> players;

        public int MaxPlayers
        {
            get => maxPlayers;
            set
            {
                maxPlayers = value;
                OnPropertyChanged(nameof(MaxPlayers));
            }
        }

        public IDictionary<RoleVisual, int> Roles
        {
            get => roles;
            set
            {
                roles = value;
                OnPropertyChanged(nameof(Roles));
            }
        }

        public ObservableCollection<LobbyPlayer> Players
        {
            get => players;
            set
            {
                players = value;
                OnPropertyChanged(nameof(Players));
            }
        }

        public LobbySetup()
        {
            roles = new Dictionary<RoleVisual, int>();
            players = new ObservableCollection<LobbyPlayer>();
        }
    }
}