using System.Text.Json.Serialization;

namespace Net.Contexts.Connection
{
    [Serializable]
    public class DisconnectPlayerContext : SessionContext
    {
        public ulong ClientId { get; }

        [JsonConstructor]
        public DisconnectPlayerContext(ulong clientId)
        {
            ClientId = clientId;
        }
    }
}