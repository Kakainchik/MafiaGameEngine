using GameLogic.Roles;

namespace GameLogic.Model
{
    public class InvestigateDetail : BaseDetail
    {
        public Role Info { get; set; }

        public InvestigateDetail(ActionType type,
            Player executor,
            Player primaryTarget,
            Role info)
            : base(type, executor, primaryTarget)
        {
            Info = info;
        }
    }
}