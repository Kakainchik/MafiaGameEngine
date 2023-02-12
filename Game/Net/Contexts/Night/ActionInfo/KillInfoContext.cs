using GameLogic.Actions.ActionTemplates;

namespace Net.Contexts.Night.ActionInfo
{
    [Serializable]
    public abstract class KillInfoContext : NightInfoContext
    {
        public KillInfo Info { get; }
        public bool ToTarget { get; }

        public KillInfoContext(KillInfo info, bool toTarget)
        {
            Info = info;
            ToTarget = toTarget;
        }
    }
}