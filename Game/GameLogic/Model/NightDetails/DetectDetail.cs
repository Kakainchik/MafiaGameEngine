using GameLogic.Actions.ActionTemplates;

namespace GameLogic.Model
{
    public class DetectDetail : BaseDetail
    {
        public DetectInfo Info { get; }

        public DetectDetail(
            ActionType type,
            Player executor,
            Player primaryTarget,
            DetectInfo info) 
            : base(type, executor, primaryTarget)
        {
            Info = info;
        }
    }
}