using System.Text.Json.Serialization;

namespace Net.Contexts.Day
{
    [Serializable]
    public class WarningVoteContext : SessionContext
    {
        public ulong? WarnedPlayerId { get; }
        public bool IsWarned { get; }

        [JsonConstructor]
        public WarningVoteContext(ulong? warnedPlayerid, bool isWarned)
        {
            WarnedPlayerId = warnedPlayerid;
            IsWarned = isWarned;
        }
    }
}