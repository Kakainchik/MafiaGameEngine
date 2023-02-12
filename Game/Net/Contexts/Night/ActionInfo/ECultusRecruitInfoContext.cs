using GameLogic.Actions.ActionTemplates;

namespace Net.Contexts.Night.ActionInfo
{
    [Serializable]
    public class ECultusRecruitInfoContext : NightInfoContext
    {
        public RecruitInfo Info { get; }

        public ECultusRecruitInfoContext(RecruitInfo info)
        {
            Info = info;
        }
    }
}