using System.Text.Json.Serialization;

namespace Net.Contexts.Day
{
    [Serializable]
    public class WarningVoteContext : SessionContext
    {
        public Guid WarnedPlayerId { get; }
        public bool IsWarned { get; }

        [JsonConstructor]
        public WarningVoteContext(Guid warnedPlayerid, bool isWarned)
        {
            WarnedPlayerId = warnedPlayerid;
            IsWarned = isWarned;
        }
    }
}