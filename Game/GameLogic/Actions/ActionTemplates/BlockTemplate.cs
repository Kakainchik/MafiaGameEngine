using GameLogic.Model;
using GameLogic.Roles;

namespace GameLogic.Actions.ActionTemplates
{
    public class BlockTemplate : ActionTemplate
    {
        public BlockTemplate(IEnumerable<ActionLog> states) : base(states)
        {

        }

        public override IEnumerable<BaseDetail> Convert()
        {
            //Find all blockers
            var block = logs.Where(
                a => a.Executor is ProstituteRole
                || a.Executor is WhoreRole);

            foreach(var action in block)
            {
                BlockInfo info;
                Player executor = action.Executor.Owner;
                Player target = action.PrimaryTarget.Owner;
                ActionType type = ActionType.ESCORT_BLOCK;

                if(action.IsBlocked)
                {
                    if(action.Executor == action.PrimaryTarget)
                    {
                        //Cause of blocking self
                        info = BlockInfo.BLOCK_SELF;
                    }
                    else
                    {
                        info = BlockInfo.BLOCKED_BY_OTHER;
                    }
                }
                else
                {
                    info = BlockInfo.BLOCK_SOMEONE;
                }

                yield return new BlockDetail(type, executor, target, info);
            }
        }
    }

    public enum BlockInfo : byte
    {
        BLOCK_SOMEONE,
        BLOCK_SELF,
        BLOCKED_BY_OTHER
    }
}