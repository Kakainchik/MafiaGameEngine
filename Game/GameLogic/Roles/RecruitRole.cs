using GameLogic.Attributes;
using GameLogic.Model;

namespace GameLogic.Roles
{
    /// <summary>
    /// Роль рекрута. Временно относится к <see cref="Team.TOWN">Городу</see>.
    /// Нет никаких способностей. Вербуется в <see cref="MafiaRole">мафиози</see>.
    /// </summary>
    [Executor(ExecutorType.NONE)]
    [Category(RCategory.GOVERMENT)]
    [Team(Team.TOWN)]
    public class RecruitRole : Role
    {
        public override bool IsUnique => false;

        public RecruitRole()
        {

        }

        public override ActionLog RecruitMafia()
        {
            if(IsAlive)
            {
                //Recruit to Mafia
                Owner.ChangeRole(new MafiaRole());
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
            return $"{typeof(RecruitRole).Name}";
        }
    }
}