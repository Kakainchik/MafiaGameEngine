using System.Text.Json.Serialization;

namespace Net.Contexts.Night
{
    [Serializable]
    public class SendDActionContext : SendActionContext
    {
        public ulong SecondaryTarget { get; }

        [JsonConstructor]
        public SendDActionContext(ulong primaryTarget, ulong secondaryTarget)
            : base(primaryTarget)
        {
            SecondaryTarget = secondaryTarget;
        }
    }
}