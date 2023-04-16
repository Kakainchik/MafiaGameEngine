namespace WPFApplication.Model
{
    public class HostLobbySetup : LobbySetup
    {
        private string? cityName;

        public string? CityName
        {
            get => cityName;
            set
            {
                cityName = value;
                OnPropertyChanged(nameof(CityName));
            }
        }

        public HostLobbySetup() : base()
        {
            MaxPlayers = 5;
        }
    }
}