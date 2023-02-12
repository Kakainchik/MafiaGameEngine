using GameLogic.Actions;
using GameLogic.Attributes;
using GameLogic.Interfaces;
using GameLogic.Model;

namespace GameLogic.Roles
{
    /// <summary>
    /// Роль хирурга. Относится к <see cref="Team.MAFIA">Мафии</see>.
    /// Каждую ночь может защитить человека от смерти в течение данной ночи.
    /// </summary>
    [Executor(ExecutorType.TARGET)]
    [Category(RCategory.SUPPORT)]
    [ChatScope(ChatScope.MAFIA, CanWrite = true)]
    [Team(Team.MAFIA)]
    public class SurgeonRole : Role, IExecutor
    {
        private IAction healAction;

        public override bool IsUnique => false;

        public SurgeonRole()
        {
            
        }

        public IAction PrepareAction(Role subject)
        {
            healAction = new HealAction(subject, this);

            return healAction;
        }

        public void DisposeAction()
        {
            healAction = null;
        }

        public override string ToString()
        {
            return $"{healAction.Priority}: {typeof(SurgeonRole).Name}";
        }
    }
}