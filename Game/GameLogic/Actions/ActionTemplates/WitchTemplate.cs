using GameLogic.Model;
using GameLogic.Roles;

namespace GameLogic.Actions.ActionTemplates
{
    public class WitchTemplate : ActionTemplate
    {
        public WitchTemplate(IEnumerable<ActionLog> logs) : base(logs)
        {

        }

        public override IEnumerable<BaseDetail> Convert()
        {
            var witch = logs.Where(s => s.Executor is WitchRole);

            foreach(var action in witch)
            {
                Player executor = action.Executor.Owner;
                Player target = action.PrimaryTarget.Owner;
                ActionType type = ActionType.WITCH_CONTROL;

                yield return new BaseDetail(type, executor, target);
            }
        }
    }
}