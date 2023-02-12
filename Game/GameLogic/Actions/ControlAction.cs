using GameLogic.Model;
using GameLogic.Roles;

namespace GameLogic.Actions
{
    public class ControlAction : BaseDoubleAction
    {
        public override APriority Priority => APriority.B1;

        public ControlAction(Role primary, Role secondary, Role executor)
            : base(primary, secondary, executor)
        {

        }

        public override ActionLog AccomplishAction()
        {
            //Cannot be blocked due to highest priority
            //Always success even if the target is not executor type
            return new DoubleActionLog
            {
                Executor = executor,
                PrimaryTarget = PrimaryTarget,
                SecondaryTarget = SecondaryTarget,
                Success = true
            };
        }
    }
}