using GameLogic.Actions;
using GameLogic.Attributes;
using GameLogic.Interfaces;
using GameLogic.Model;

namespace GameLogic.Roles
{
    /// <summary>
    /// Роль шлюхи. Относится к <see cref="Team.MAFIA">Мафии</see>.
    /// Каждую ночь может заблокировать действия другого человека.
    /// </summary>
    [Executor(ExecutorType.TARGET)]
    [Category(RCategory.SUPPORT)]
    [ChatScope(ChatScope.MAFIA, CanWrite = true)]
    [Team(Team.MAFIA)]
    public class WhoreRole : Role, IExecutor
    {
        private IAction blockAction;

        public override bool IsUnique => false;

        public WhoreRole()
        {

        }

        public IAction PrepareAction(Role whom)
        {
            blockAction = new BlockAction(whom, this);

            return blockAction;
        }

        public void DisposeAction()
        {
            blockAction = null;
        }

        public override string ToString()
        {
            return $"{blockAction.Priority}: {typeof(WhoreRole).Name}";
        }
    }
}