using GameLogic.Actions.ActionTemplates;
using System.Text.Json.Serialization;

namespace Net.Contexts.Night.ActionInfo
{
    [Serializable]
    public class SerialKillerInfoContext : KillInfoContext
    {
        [JsonConstructor]
        public SerialKillerInfoContext(KillInfo info, bool toTarget)
            : base(info, toTarget)
        {
            //There is no data,
            //just show that was serial killer's kill
        }
    }
}