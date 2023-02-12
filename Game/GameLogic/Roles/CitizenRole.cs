using GameLogic.Attributes;
using GameLogic.Model;

namespace GameLogic.Roles
{
    /// <summary>
    /// Роль горожанина. Относится к <see cref="Team.TOWN">Городу</see>.
    /// Нет никаких способностей.
    /// </summary>
    [Executor(ExecutorType.NONE)]
    [Category(RCategory.GOVERMENT)]
    [Team(Team.TOWN)]
    public class CitizenRole : Role
    {
        public override bool IsUnique => false;

        public CitizenRole()
        {
            
        }

        public override string ToString()
        {
            return $"{typeof(CitizenRole).Name}";
        }
    }
}