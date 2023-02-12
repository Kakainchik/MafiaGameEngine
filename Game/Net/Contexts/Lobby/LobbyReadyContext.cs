namespace Net.Contexts.Lobby
{
    [Serializable]
    public class LobbyReadyContext : SessionContext
    {
        public bool IsReady { get; }

        public LobbyReadyContext(bool isReady)
        {
            IsReady = isReady;
        }
    }
}