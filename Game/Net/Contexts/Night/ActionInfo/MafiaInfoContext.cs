using GameLogic.Actions.ActionTemplates;

namespace Net.Contexts.Night.ActionInfo
{
    [Serializable]
    public class MafiaInfoContext : KillInfoContext
    {
        public MafiaInfoContext(KillInfo info, bool toTarget)
            : base(info, toTarget)
        {
            //There is not data,
            //just show that was mafia's kill
        }
    }
}