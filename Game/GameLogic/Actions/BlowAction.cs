using GameLogic.Interfaces;
using GameLogic.Model;
using GameLogic.Roles;

namespace GameLogic.Actions
{
    public class BlowAction : BaseAction
    {
        public override APriority Priority => APriority.B32;

        public BlowAction(Role primaryTarget, Role executor)
            : base(primaryTarget, executor)
        {

        }

        public override ActionLog AccomplishAction()
        {
            LogBuilder builder;
            if(executor.IsBlocked)
            {
                builder = new LogBuilder();
                return builder.BindTarget(PrimaryTarget)
                    .BindExecutor(executor)
                    .IsBlocked()
                    .Build();
            }
            builder = new LogBuilder(PrimaryTarget.Kill((IExecutor)executor))
                .BindExecutor(executor);
            return builder.Build();
        }
    }
}