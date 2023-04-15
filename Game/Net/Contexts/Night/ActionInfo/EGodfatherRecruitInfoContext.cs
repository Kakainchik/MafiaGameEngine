using GameLogic.Actions.ActionTemplates;
using System.Text.Json.Serialization;

namespace Net.Contexts.Night.ActionInfo
{
    [Serializable]
    public class EGodfatherRecruitInfoContext : NightInfoContext
    {
        public RecruitInfo Info { get; }

        [JsonConstructor]
        public EGodfatherRecruitInfoContext(RecruitInfo info)
        {
            Info = info;
        }
    }
}