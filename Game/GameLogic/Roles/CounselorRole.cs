using GameLogic.Actions;
using GameLogic.Attributes;
using GameLogic.Interfaces;
using GameLogic.Model;

namespace GameLogic.Roles
{
    /// <summary>
    /// Роль советника. Относится к <see cref="Team.MAFIA">Мафии</see>.
    /// Каждую ночь может изучить одного человека, вследствие чего узнать его роль.
    /// </summary>
    [Executor(ExecutorType.TARGET)]
    [Category(RCategory.INVESTIGATE)]
    [ChatScope(ChatScope.MAFIA, CanWrite = true)]
    [Team(Team.MAFIA)]
    public class CounselorRole : Role, IExecutor
    {
        private IAction investigateAction;

        public override bool IsUnique => false;

        public CounselorRole()
        {
            
        }

        public IAction PrepareAction(Role subject)
        {
            investigateAction = new InvestigateAction(subject, this);

            return investigateAction;
        }

        public void DisposeAction()
        {
            investigateAction = null;
        }

        public override string ToString()
        {
            return $"{investigateAction.Priority}: {typeof(CounselorRole).Name}";
        }
    }
}