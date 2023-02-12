using GameLogic.Model;
using GameLogic.Roles;

namespace GameLogic.Actions
{
    public class RecruitMafiaAction : RecruitAction
    {
        public RecruitMafiaAction(Role recruitTarget, Role executor) : 
            base(recruitTarget, executor)
        {

        }

        public override ActionLog AccomplishAction()
        {
            LogBuilder builder;
            if(Executor.IsBlocked)
            {
                builder = new LogBuilder();
                return builder.BindTarget(PrimaryTarget)
                    .BindExecutor(Executor)
                    .IsBlocked()
                    .Build();
            }
            builder = new LogBuilder(PrimaryTarget.RecruitMafia())
                .BindExecutor(Executor);
            return builder.Build();
        }
    }
}