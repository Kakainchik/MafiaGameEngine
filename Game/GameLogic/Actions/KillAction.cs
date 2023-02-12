using GameLogic.Interfaces;
using GameLogic.Model;
using GameLogic.Roles;

namespace GameLogic.Actions
{
    public class KillAction : BaseAction
    {
        public override APriority Priority => APriority.B4;

        public KillAction(Role blockableTarget, Role executor)
            : base(blockableTarget, executor)
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