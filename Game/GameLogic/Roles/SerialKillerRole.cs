using GameLogic.Actions;
using GameLogic.Attributes;
using GameLogic.Interfaces;
using GameLogic.Model;

namespace GameLogic.Roles
{
    /// <summary>
    /// Роль серийного убийцы. Самостоятельный и относится к <see cref="Team.NEUTRAL"/>.
    /// Каждую ночь может убить кого-то.
    /// </summary>
    [Executor(ExecutorType.TARGET)]
    [Category(RCategory.KILLING)]
    [Team(Team.SERIAL_KILLER)]
    public class SerialKillerRole : Role, IExecutor
    {
        private IAction killAction;

        public override bool IsUnique => false;

        public SerialKillerRole()
        {
            
        }

        public IAction PrepareAction(Role subject)
        {
            killAction = new KillAction(subject, this);

            return killAction;
        }

        public void DisposeAction()
        {
            killAction = null;
        }

        public override string ToString()
        {
            return $"{killAction.Priority}: {typeof(SerialKillerRole).Name}";
        }
    }
}