using GameLogic.Actions.ActionTemplates;

namespace Net.Contexts.Night.ActionInfo
{
    [Serializable]
    public class SerialKillerInfoContext : KillInfoContext
    {
        public SerialKillerInfoContext(KillInfo info, bool toTarget)
            : base(info, toTarget)
        {
            //There is no data,
            //just show that was serial killer's kill
        }
    }
}