using GameLogic.Actions;
using GameLogic.Attributes;
using GameLogic.Interfaces;
using GameLogic.Model;

namespace GameLogic.Roles
{
    /// <summary>
    /// Роль детектива. Относится к <see cref="Team.TOWN">Городу</see>.
    /// Каждую ночь может изучить одного человека, вследствие чего
    /// узнать полностью его роль, узнаёт сразу же.
    /// Крёстного отца видит как мафиози. Лидера культа видит как культиста.
    /// </summary>
    [Executor(ExecutorType.TARGET)]
    [Category(RCategory.INVESTIGATE)]
    [Team(Team.TOWN)]
    public class DetectiveRole : Role, IExecutor
    {
        private IAction investigateAction;

        public override bool IsUnique => false;

        public DetectiveRole()
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
            return $"{investigateAction.Priority}: {typeof(DetectiveRole).Name}";
        }
    }
}