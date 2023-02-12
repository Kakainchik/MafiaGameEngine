using GameLogic.Actions.ActionTemplates;

namespace Net.Contexts.Night.ActionInfo
{
    [Serializable]
    public class EGodfatherRecruitInfoContext : NightInfoContext
    {
        public RecruitInfo Info { get; }

        public EGodfatherRecruitInfoContext(RecruitInfo info)
        {
            Info = info;
        }
    }
}