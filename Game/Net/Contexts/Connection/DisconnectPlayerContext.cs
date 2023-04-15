using System.Text.Json.Serialization;

namespace Net.Contexts.Connection
{
    [Serializable]
    public class DisconnectPlayerContext : SessionContext
    {
        public Guid SessionId { get; }

        [JsonConstructor]
        public DisconnectPlayerContext(Guid sessionId)
        {
            SessionId = sessionId;
        }
    }
}