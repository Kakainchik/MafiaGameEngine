using GameLogic.Actions;
using GameLogic.Attributes;
using GameLogic.Interfaces;
using GameLogic.Model;

namespace GameLogic.Roles
{
    /// <summary>
    /// Роль стандартной мафии. Относится к <see cref="Team.MAFIA">Мафии</see>.
    /// Каждую ночь должен решить, кого убить.
    /// </summary>
    [Executor(ExecutorType.TARGET)]
    [Category(RCategory.KILLING)]
    [ChatScope(ChatScope.MAFIA, CanWrite = true)]
    [Team(Team.MAFIA)]
    public class MafiaRole : Role, IExecutor
    {
        private IAction killAction;

        public override bool IsUnique => false;

        public MafiaRole()
        {

        }

        public IAction PrepareAction(Role whom)
        {
            killAction = new KillAction(whom, this);

            return killAction;
        }

        public void DisposeAction()
        {
            killAction = null;
        }

        public override string ToString()
        {
            return $"{killAction.Priority}: {typeof(MafiaRole).Name}";
        }
    }
}