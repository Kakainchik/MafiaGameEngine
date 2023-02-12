using GameLogic.Actions;
using GameLogic.Attributes;
using GameLogic.Interfaces;
using GameLogic.Model;

namespace GameLogic.Roles
{
    /// <summary>
    /// Роль проститутки. Временно относится к <see cref="Team.TOWN">Городу</see>.
    /// Каждую ночь может заблокировать действия другого человека.
    /// Вербуется в <see cref="WhoreRole">шлюху</see>.
    /// </summary>
    [Executor(ExecutorType.TARGET)]
    [Category(RCategory.SUPPORT)]
    [Team(Team.TOWN)]
    public class ProstituteRole : Role, IExecutor
    {
        private IAction blockAction;

        public override bool IsUnique => false;

        public ProstituteRole()
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

        public override ActionLog RecruitMafia()
        {
            if(IsAlive)
            {
                //Recruit to Whore
                Owner.ChangeRole(new WhoreRole());
                return new ActionLog
                {
                    Success = true,
                    PrimaryTarget = this
                };
            }
            else return new ActionLog
            {
                Success = false,
                PrimaryTarget = this
            };
        }

        public override string ToString()
        {
            return $"{blockAction.Priority}: {typeof(ProstituteRole).Name}";
        }
    }
}