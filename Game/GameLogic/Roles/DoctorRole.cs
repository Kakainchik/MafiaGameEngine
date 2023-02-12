using GameLogic.Actions;
using GameLogic.Attributes;
using GameLogic.Interfaces;
using GameLogic.Model;

namespace GameLogic.Roles
{
    /// <summary>
    /// Роль доктора. Относится к <see cref="Team.TOWN">Городу</see>.
    /// Каждую ночь может защитить одного человека от смерти в течение данной ночи.
    /// </summary>
    [Executor(ExecutorType.TARGET)]
    [Category(RCategory.SUPPORT)]
    [Team(Team.TOWN)]
    public class DoctorRole : Role, IExecutor
    {
        private IAction healAction;

        public override bool IsUnique => false;

        public DoctorRole()
        {

        }

        public IAction PrepareAction(Role whom)
        {
            healAction = new HealAction(whom, this);

            return healAction;
        }

        public void DisposeAction()
        {
            healAction = null;
        }

        public override string ToString()
        {
            return $"{healAction.Priority}: {typeof(DoctorRole).Name}";
        }
    }
}