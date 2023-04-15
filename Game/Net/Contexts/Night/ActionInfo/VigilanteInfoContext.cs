using GameLogic.Actions.ActionTemplates;
using System.Text.Json.Serialization;

namespace Net.Contexts.Night.ActionInfo
{
    [Serializable]
    public class VigilanteInfoContext : KillInfoContext
    {
        [JsonConstructor]
        public VigilanteInfoContext(KillInfo info, bool toTarget)
            : base(info, toTarget)
        {
            //There is no data,
            //just show that was vigilante's kill
        }
    }
}