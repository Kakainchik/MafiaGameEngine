using System.Text.Json.Serialization;

namespace Net.Contexts.Night
{
    [Serializable]
    public class SendActionContext : SessionContext
    {
        public Guid? PrimaryTarget { get; }
        public bool HasNonExFlag => PrimaryTarget is null;

        [JsonConstructor]
        public SendActionContext(Guid primaryTarget)
        {
            PrimaryTarget = primaryTarget;
        }

        public SendActionContext()
        {
            PrimaryTarget = null;
        }
    }
}