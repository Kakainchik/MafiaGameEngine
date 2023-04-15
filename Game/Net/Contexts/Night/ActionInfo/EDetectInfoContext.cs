using GameLogic.Actions.ActionTemplates;
using System.Text.Json.Serialization;

namespace Net.Contexts.Night.ActionInfo
{
    [Serializable]
    public class EDetectInfoContext : NightInfoContext
    {
        public DetectInfo Info { get; }

        [JsonConstructor]
        public EDetectInfoContext(DetectInfo info)
        {
            Info = info;
        }
    }
}