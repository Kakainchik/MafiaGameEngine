namespace Net.Contexts.Day
{
    [Serializable]
    public class WarningVoteContext : SessionContext
    {
        public Guid WarnedPlayerId { get; }
        public bool IsWarned { get; }

        public WarningVoteContext(Guid id, bool isWarned)
        {
            WarnedPlayerId = id;
            IsWarned = isWarned;
        }
    }
}