using GameLogic.Model;
using GameLogic.Roles;

namespace GameLogic.Actions
{
    public abstract class BaseAction : IAction
    {
        protected Role executor;

        public Role Executor => executor;
        public Role PrimaryTarget { get; set; }

        public abstract APriority Priority { get; }

        protected BaseAction(Role primarytarget, Role executor)
        {
            this.executor = executor;
            PrimaryTarget = primarytarget;
        }

        public abstract ActionLog AccomplishAction();
    }
}