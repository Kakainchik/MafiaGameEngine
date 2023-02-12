using GameLogic.Actions;
using GameLogic.Attributes;
using GameLogic.Interfaces;
using GameLogic.Model;

namespace GameLogic.Roles
{
    /// <summary>
    /// Роль лидера культа. Относится к <see cref="Team.CULTUS">Культу</see>.
    /// Каждую ночь может завербовать кого-то в культ, не может разговаривать с члена культа, но может слышать их.
    /// Не может завербовать крёстного отца.
    /// </summary>
    [Executor(ExecutorType.TARGET)]
    [Category(RCategory.GOVERMENT)]
    [ChatScope(ChatScope.CULTUS, CanWrite = false)]
    [Team(Team.CULTUS)]
    public class CultusLeaderRole : Role, IExecutor
    {
        private IAction recruitAction;

        public override bool IsUnique => true;

        public CultusLeaderRole()
        {
            
        }

        public IAction PrepareAction(Role subject)
        {
            recruitAction = new RecruitCultusAction(subject, this);

            return recruitAction;
        }

        public void DisposeAction()
        {
            recruitAction = null;
        }

        public override ActionLog RecruitCultus()
        {
            //Cannot recruit self
            return new ActionLog
            {
                Success = false,
                PrimaryTarget = this
            };
        }

        public override string ToString()
        {
            return $"{recruitAction.Priority}: {typeof(CultusLeaderRole).Name}";
        }
    }
}