using GameLogic.Actions;
using GameLogic.Attributes;
using GameLogic.Interfaces;
using GameLogic.Model;

namespace GameLogic.Roles
{
    /// <summary>
    /// Роль полицейского. Относится к <see cref="Team.TOWN">Городу</see>.
    /// Каждую ночь может проверить одного человека и узнать его принадлежность
    /// к какой-либо <see cref="Team">стороне</see>, узнаёт на следующую ночь.
    /// Крёстного отца видит как Город.
    /// </summary>
    [Executor(ExecutorType.TARGET)]
    [Category(RCategory.INVESTIGATE)]
    [Team(Team.TOWN)]
    public class PolicemanRole : Role, IExecutor
    {
        private IAction investigateAction;

        public override bool IsUnique => false;

        public PolicemanRole()
        {

        }

        public IAction PrepareAction(Role whom)
        {
            investigateAction = new InvestigateAction(whom, this);

            return investigateAction;
        }

        public void DisposeAction()
        {
            investigateAction = null;
        }

        public override string ToString()
        {
            return $"{investigateAction.Priority}: {typeof(PolicemanRole).Name}";
        }
    }
}