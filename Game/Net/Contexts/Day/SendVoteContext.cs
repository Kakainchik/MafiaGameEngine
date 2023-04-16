using System.Text.Json.Serialization;

namespace Net.Contexts.Day
{
    [Serializable]
    public class SendVoteContext : SessionContext
    {
        public ulong? TargetId { get; }

        [JsonConstructor]
        public SendVoteContext(ulong? targetId)
        {
            TargetId = targetId;
        }
    }
}