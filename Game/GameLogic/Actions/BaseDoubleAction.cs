using GameLogic.Roles;

namespace GameLogic.Actions
{
    public abstract class BaseDoubleAction : BaseAction, IDoubleAction
    {
        public Role SecondaryTarget { get; set; }

        protected BaseDoubleAction(Role primarytarget, Role secondary, Role executor) 
            : base(primarytarget, executor)
        {
            SecondaryTarget = secondary;
        }
    }
}