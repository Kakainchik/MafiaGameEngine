using GameLogic.Attributes;
using GameLogic.Model;

namespace GameLogic.Roles
{
    /// <summary>
    /// Роль медиума. Относится к <see cref="Team.UNDEAD">Нежити</see>.
    /// Ночью может обращаться к мёртвым.
    /// Уникален.
    /// </summary>
    [Executor(ExecutorType.NONE)]
    [Category(RCategory.GOVERMENT)]
    [ChatScope(ChatScope.UNDEAD, CanWrite = true)]
    [ChatScope(ChatScope.DEAD, CanWrite = true)]
    [Team(Team.UNDEAD)]
    public class PsychicRole : Role
    {
        public override bool IsUnique => true;

        public PsychicRole()
        {

        }

        public override string ToString()
        {
            return $"{typeof(PsychicRole).Name}";
        }
    }
}