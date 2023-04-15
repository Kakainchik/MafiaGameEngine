using System.Text.Json.Serialization;

namespace Net.Contexts.Day
{
    [Serializable]
    public class SendVoteContext : SessionContext
    {
        public Guid TargetId { get; }

        [JsonConstructor]
        public SendVoteContext(Guid targetId)
        {
            TargetId = targetId;
        }
    }
}