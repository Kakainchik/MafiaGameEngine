using GameLogic.Actions.ActionTemplates;

namespace GameLogic.Model
{
    public class RecruitDetail : BaseDetail
    {
        public RecruitInfo Info { get; }

        public RecruitDetail(ActionType type,
            Player executor,
            Player primaryTarget,
            RecruitInfo info)
            : base(type, executor, primaryTarget)
        {
            Info = info;
        }
    }
}