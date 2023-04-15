using GameLogic.Actions.ActionTemplates;
using System.Text.Json.Serialization;

namespace Net.Contexts.Night.ActionInfo
{
    [Serializable]
    public abstract class KillInfoContext : NightInfoContext
    {
        public KillInfo Info { get; }
        public bool ToTarget { get; }

        [JsonConstructor]
        public KillInfoContext(KillInfo info, bool toTarget)
        {
            Info = info;
            ToTarget = toTarget;
        }
    }
}