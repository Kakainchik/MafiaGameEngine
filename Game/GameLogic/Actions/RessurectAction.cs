using GameLogic.Model;
using GameLogic.Roles;

namespace GameLogic.Actions
{
    public class RessurectAction : BaseAction
    {
        public override APriority Priority => APriority.B128;

        public RessurectAction(Role blockableTarget, Role executor)
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
            builder = new LogBuilder(PrimaryTarget.Ressurect())
                .BindExecutor(executor);
            return builder.Build();
        }
    }
}