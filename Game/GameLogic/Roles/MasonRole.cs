using GameLogic.Attributes;
using GameLogic.Model;

namespace GameLogic.Roles
{
    /// <summary>
    /// Роль масона. Относится к <see cref="Team.TOWN">Городу</see>.
    /// Может общаться с другими масонами ночью.
    /// </summary>
    [Executor(ExecutorType.NONE)]
    [Category(RCategory.GOVERMENT)]
    [ChatScope(ChatScope.MASON, CanWrite = true)]
    [Team(Team.TOWN)]
    public class MasonRole : Role
    {
        public override bool IsUnique => false;

        public MasonRole()
        {
            
        }
        
        public override string ToString()
        {
            return $"{typeof(MasonRole).Name}";
        }
    }
}