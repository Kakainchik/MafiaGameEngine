using GameLogic.Actions.ActionTemplates;

namespace GameLogic.Model
{
    public class KillDetail : BaseDetail
    {
        public KillInfo Info { get; set; }

        public KillDetail(
            ActionType type,
            Player executor,
            Player primaryTarget,
            KillInfo info) 
            : base(type, executor, primaryTarget)
        {
            Info = info;
        }
    }
}