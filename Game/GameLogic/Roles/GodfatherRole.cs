using GameLogic.Actions;
using GameLogic.Attributes;
using GameLogic.Interfaces;
using GameLogic.Model;

namespace GameLogic.Roles
{
    /// <summary>
    /// Роль крёстного отца. Относится к <see cref="Team.MAFIA">Мафии</see>.
    /// Каждую ночь приказывает <see cref="MafiaRole">мафиози</see>, 
    /// кого им убить, также может вербовать <see cref="RecruitRole">рекрутов</see> 
    /// и <see cref="ProstituteRole">проституток</see>.
    /// Не может быть убит ночью.
    /// </summary>
    [Executor(ExecutorType.TARGET)]
    [Category(RCategory.GOVERMENT)]
    [ChatScope(ChatScope.MAFIA, CanWrite = true)]
    [Team(Team.MAFIA)]
    public class GodfatherRole : Role, IExecutor
    {
        private IAction orderAction;
        private IAction recruitAction;

        public override bool IsUnique => true;

        public GodfatherRole()
        {
            
        }

        public IAction PrepareAction(Role subject)
        {
            recruitAction = new RecruitMafiaAction(subject, this);

            return recruitAction;
        }

        public void DisposeAction()
        {
            orderAction = null;
            recruitAction = null;
        }

        public override ActionLog Kill(IExecutor killer)
        {
            //Cannot die during the night
            return new ActionLog()
            {
                Success = false,
                PrimaryTarget = this
            };
        }

        public override ActionLog RecruitCultus()
        {
            //Cannot be recruit to cultus
            return new ActionLog
            {
                Success = false,
                PrimaryTarget = this
            };
        }

        public override string ToString()
        {
            return $"{recruitAction.Priority}: {typeof(GodfatherRole).Name}";
        }
    }
}