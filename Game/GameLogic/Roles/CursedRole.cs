using GameLogic.Attributes;
using GameLogic.Interfaces;
using GameLogic.Model;

namespace GameLogic.Roles
{
    /// <summary>
    /// Роль проклятого. Временно относится к <see cref="Team.TOWN">Городу</see>.
    /// Если умрёт ночью, то превратится в <see cref="ZombieRole">Зомби</see>.
    /// </summary>
    [Executor(ExecutorType.NONE)]
    [Category(RCategory.GOVERMENT)]
    [Team(Team.TOWN)]
    public class CursedRole : Role
    {
        public override bool IsUnique => false;

        public CursedRole()
        {
            
        }

        public override ActionLog Kill(IExecutor killer)
        {
            if(IsAlive)
            {
                IsAlive = false;
                //Change role to Zombie
                Owner.ChangeRole(new ZombieRole());
                return new ActionLog
                {
                    Success = true,
                    PrimaryTarget = this
                };
            }
            //If already dead - kill next Zombie role of the player
            else return Owner.Role.Kill(killer);
        }

        public override ActionLog Heal()
        {
            //Cannot be healed, instantly has dead after killing
            return new ActionLog
            {
                Success = false,
                PrimaryTarget = this
            };
        }

        public override string ToString()
        {
            return $"{typeof(CursedRole).Name}";
        }
    }
}