using GameLogic.Model;
using GameLogic.Roles;

namespace GameLogic.Actions
{
    public abstract class RecruitAction : BaseAction
    {
        public override APriority Priority => APriority.B128;

        public RecruitAction(Role blockableTarget, Role executor)
            : base(blockableTarget, executor)
        {

        }
    }
}