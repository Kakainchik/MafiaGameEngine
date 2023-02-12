namespace Net.Contexts.Day
{
    [Serializable]
    public class SendVoteContext : SessionContext
    {
        public Guid TargetId { get; }

        public SendVoteContext(Guid targetId)
        {
            TargetId = targetId;
        }
    }
}