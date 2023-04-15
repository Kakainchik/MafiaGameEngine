using System.Text.Json.Serialization;

namespace Net.Contexts.Night
{
    [Serializable]
    public class SendDActionContext : SendActionContext
    {
        public Guid SecondaryTarget { get; }

        [JsonConstructor]
        public SendDActionContext(Guid primaryTarget, Guid secondaryTarget)
            : base(primaryTarget)
        {
            SecondaryTarget = secondaryTarget;
        }
    }
}