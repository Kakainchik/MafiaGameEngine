using GameLogic.Model;
using GameLogic.Roles;

namespace GameLogic.Actions
{
    public class DriveAction : BaseDoubleAction
    {
        public override APriority Priority => APriority.B1;

        public DriveAction(Role primary, Role secondary, Role executor)
            : base(primary, secondary, executor)
        {

        }

        public override ActionLog AccomplishAction()
        {
            //Cannot be blocked due to highest priority
            //Always success
            return new DoubleActionLog
            {
                Success = true,
                PrimaryTarget = PrimaryTarget,
                SecondaryTarget = SecondaryTarget,
                Executor = executor
            };
        }
    }
}