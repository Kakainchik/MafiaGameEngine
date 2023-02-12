using GameLogic.Model;
using GameLogic.Roles;

namespace GameLogic.Actions.ActionTemplates
{
    public class RessurectTemplate : ActionTemplate
    {
        public RessurectTemplate(IEnumerable<ActionLog> logs) : base(logs)
        {

        }

        public override IEnumerable<BaseDetail> Convert()
        {
            var ressurect = logs.Where(s => s.Executor is ZombieRole)
                .Where(s => s.Success);

            foreach(var action in ressurect)
            {
                ActionType type = ActionType.RESSURECT;
                Player executor = action.Executor.Owner;
                Player target = action.PrimaryTarget.Owner;

                yield return new BaseDetail(type, executor, target);
            }
        }
    }
}