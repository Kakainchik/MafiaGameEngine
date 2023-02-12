namespace Net.Models
{
    [Serializable]
    public class LobbyPlayer
    {
        public string Username { get; }
        public bool IsReady { get; set; }

        public LobbyPlayer(string username, bool isReady = false)
        {
            Username = username;
            IsReady = isReady;
        }
    }
}