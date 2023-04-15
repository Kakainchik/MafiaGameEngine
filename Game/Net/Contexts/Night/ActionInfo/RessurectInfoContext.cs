using System.Text.Json.Serialization;

namespace Net.Contexts.Night.ActionInfo
{
    [Serializable]
    public class RessurectInfoContext : NightInfoContext
    {
        public bool ToTarget { get; }

        [JsonConstructor]
        public RessurectInfoContext(bool toTarget)
        {
            ToTarget = toTarget;
        }
    }
}