using GameLogic.Attributes;
using GameLogic.Model;

namespace GameLogic.Roles
{
    /// <summary>
    /// Роль культиста. Относится к <see cref="Team.CULTUS">Культу</see>.
    /// Ночью может разговаривать с другими культистами, но личность Лидера остаётся тайной.
    /// </summary>
    [Executor(ExecutorType.NONE)]
    [Category(RCategory.GOVERMENT)]
    [ChatScope(ChatScope.CULTUS, CanWrite = true)]
    [Team(Team.CULTUS)]
    public class CultistRole : Role
    {
        public override bool IsUnique => false;

        public CultistRole()
        {
            
        }

        public override ActionLog RecruitCultus()
        {
            //Already is in cultus, no needness
            return new ActionLog
            {
                Success = false,
                PrimaryTarget = this
            };
        }

        public override string ToString()
        {
            return $"{typeof(CultistRole).Name}";
        }
    }
}