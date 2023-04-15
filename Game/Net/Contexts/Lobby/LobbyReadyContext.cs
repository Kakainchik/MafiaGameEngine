using System.Text.Json.Serialization;

namespace Net.Contexts.Lobby
{
    [Serializable]
    public class LobbyReadyContext : SessionContext
    {
        public bool IsReady { get; }

        [JsonConstructor]
        public LobbyReadyContext(bool isReady)
        {
            IsReady = isReady;
        }
    }
}