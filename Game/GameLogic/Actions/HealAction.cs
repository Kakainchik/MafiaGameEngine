using GameLogic.Model;
using GameLogic.Roles;

namespace GameLogic.Actions
{
    public class HealAction : BaseAction
    {
        public override APriority Priority => APriority.B8;

        public HealAction(Role blockableTarget, Role executor)
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
            builder = new LogBuilder(PrimaryTarget.Heal())
                .BindExecutor(executor);
            return builder.Build();
        }
    }
}