using System.Text.Json.Serialization;

namespace Net.Contexts.Lobby
{
    [Serializable]
    public class LobbyMaxPlayerContext : SessionContext
    {
        public int Quantity { get; }

        [JsonConstructor]
        public LobbyMaxPlayerContext(int quantity)
        {
            Quantity = quantity;
        }
    }
}