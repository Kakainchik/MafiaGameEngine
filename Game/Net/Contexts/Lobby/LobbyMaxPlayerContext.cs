namespace Net.Contexts.Lobby
{
    [Serializable]
    public class LobbyMaxPlayerContext : SessionContext
    {
        public int Quantity { get; }

        public LobbyMaxPlayerContext(int quantity)
        {
            Quantity = quantity;
        }
    }
}