using GameLogic.Model;
using GameLogic.Roles;

namespace GameLogic.Actions
{
    public class InvestigateAction : BaseAction
    {
        public override APriority Priority => APriority.B16;

        public InvestigateAction(Role blockableTarget, Role executor)
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
            builder = new LogBuilder()
                .BindTarget(PrimaryTarget)
                .BindExecutor(executor)
                .IsSuccess();
            return builder.Build();
        }
    }
}