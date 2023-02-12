namespace Net.Contexts.Connection
{
    [Serializable]
    public class DisconnectPlayerContext : SessionContext
    {
        public Guid SessionId { get; }

        public DisconnectPlayerContext(Guid sessionId)
        {
            SessionId = sessionId;
        }
    }
}