namespace Net.Contexts.Night.ActionInfo
{
    [Serializable]
    public class HealInfoContext : NightInfoContext
    {
        public bool ToTarget { get; }

        public HealInfoContext(bool toTarget)
        {
            ToTarget = toTarget;
        }
    }
}