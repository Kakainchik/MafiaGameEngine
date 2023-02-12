using GameLogic.Actions.ActionTemplates;

namespace Net.Contexts.Night.ActionInfo
{
    [Serializable]
    public class VigilanteInfoContext : KillInfoContext
    {
        public VigilanteInfoContext(KillInfo info, bool toTarget)
            : base(info, toTarget)
        {
            //There is no data,
            //just show that was vigilante's kill
        }
    }
}