namespace Net.Contexts.Night.ActionInfo
{
    [Serializable]
    public class RessurectInfoContext : NightInfoContext
    {
        public bool ToTarget { get; }

        public RessurectInfoContext(bool toTarget)
        {
            ToTarget = toTarget;
        }
    }
}