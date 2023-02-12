using GameLogic.Actions;
using GameLogic.Attributes;
using GameLogic.Interfaces;
using GameLogic.Model;

namespace GameLogic.Roles
{
    /// <summary>
    /// Роль зомби. Относится к <see cref="Team.UNDEAD">Нежити</see>.
    /// Каждую ночь решает, кого возродить из мёртвых.
    /// </summary>
    [Executor(ExecutorType.TARGET)]
    [Category(RCategory.SUPPORT)]
    [ChatScope(ChatScope.UNDEAD, CanWrite = true)]
    [Team(Team.UNDEAD)]
    public class ZombieRole : Role, IExecutor
    {
        private IAction ressurectAction;

        public override bool IsUnique => false;

        public ZombieRole()
        {
            
        }

        public IAction PrepareAction(Role whom)
        {
            ressurectAction = new RessurectAction(whom, this);

            return ressurectAction;
        }

        public void DisposeAction()
        {
            ressurectAction = null;
        }

        public override ActionLog RecruitCultus()
        {
            //Zombie cannot be recruit to cultus
            return new ActionLog
            {
                Success = false,
                PrimaryTarget = this
            };
        }

        public override ActionLog Ressurect()
        {
            //Zombie cannot be ressurected
            return new ActionLog
            {
                Success = false,
                PrimaryTarget = this
            };
        }

        public override string ToString()
        {
            return $"{ressurectAction.Priority}: {typeof(ZombieRole).Name}";
        }
    }
}