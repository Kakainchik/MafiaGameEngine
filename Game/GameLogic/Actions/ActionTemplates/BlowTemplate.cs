using GameLogic.Model;
using GameLogic.Roles;

namespace GameLogic.Actions.ActionTemplates
{
    public class BlowTemplate : ActionTemplate
    {
        public BlowTemplate(IEnumerable<ActionLog> logs) : base(logs)
        {

        }

        public override IEnumerable<BaseDetail> Convert()
        {
            //Find all terrorists with success
            var blow = logs.Where(s => s.Executor is TerroristRole)
                .Where(s => s.Success)
                .Distinct(new BlowComparer());

            foreach(var action in blow)
            {
                ActionType type = ActionType.TERRORIST_BLOW;
                Player executor = action.Executor.Owner;
                Player target = action.PrimaryTarget.Owner;

                var victims = logs.Where(s => s.Executor == action.Executor)
                    .Where(s => s.Success)
                    .Select(s => s.PrimaryTarget.Owner).ToArray();

                yield return new BlowDetail(type, executor, target, victims);
            }
        }

        private class BlowComparer : IEqualityComparer<ActionLog>
        {
            public bool Equals(ActionLog x, ActionLog y)
            {
                return object.ReferenceEquals(x.Executor, y.Executor);
            }

            public int GetHashCode(ActionLog obj)
            {
                if(object.ReferenceEquals(obj, null)) return 0;

                int hashExecutor = obj.Executor?.GetHashCode() ?? 0;

                return hashExecutor;
            }
        }
    }
}