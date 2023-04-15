using System.Text.Json.Serialization;

namespace Net.Contexts.Connection
{
    [Serializable]
    public class ConnectPlayerContext : SessionContext
    {
        public Guid PlayerId { get; }
        public string Username { get; }

        [JsonConstructor]
        public ConnectPlayerContext(Guid playerId, string username)
        {
            PlayerId = playerId;
            Username = username;
        }
    }
}