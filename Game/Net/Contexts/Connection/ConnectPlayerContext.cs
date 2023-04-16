using System.Text.Json.Serialization;

namespace Net.Contexts.Connection
{
    [Serializable]
    public class ConnectPlayerContext : SessionContext
    {
        public ulong PlayerId { get; }
        public string Username { get; }

        [JsonConstructor]
        public ConnectPlayerContext(ulong playerId, string username)
        {
            PlayerId = playerId;
            Username = username;
        }
    }
}