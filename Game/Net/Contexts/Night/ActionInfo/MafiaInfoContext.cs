using GameLogic.Actions.ActionTemplates;
using System.Text.Json.Serialization;

namespace Net.Contexts.Night.ActionInfo
{
    [Serializable]
    public class MafiaInfoContext : KillInfoContext
    {
        [JsonConstructor]
        public MafiaInfoContext(KillInfo info, bool toTarget)
            : base(info, toTarget)
        {
            //There is not data,
            //just show that was mafia's kill
        }
    }
}