using GameLogic.Actions.ActionTemplates;

namespace GameLogic.Model
{
    public class BlockDetail : BaseDetail
    {
        public BlockInfo Info { get; }

        public BlockDetail(
            ActionType type,
            Player executor,
            Player primaryTarget,
            BlockInfo info) 
            : base(type, executor, primaryTarget)
        {
            Info = info;
        }
    }
}