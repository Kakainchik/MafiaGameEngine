using GameLogic.Model;
using GameLogic.Roles;

namespace GameLogic.Actions.ActionTemplates
{
    public class InvestigateTemplate : ActionTemplate
    {
        public InvestigateTemplate(IEnumerable<ActionLog> logs) : base(logs)
        {

        }

        public override IEnumerable<BaseDetail> Convert()
        {
            //Find all investigators with success
            var investigator = logs.Where(
                s => s.Executor is CounselorRole || s.Executor is DetectiveRole)
                .Where(s => s.Success);

            foreach(var action in investigator)
            {
                Player target = action.PrimaryTarget.Owner;
                Player executor = action.Executor.Owner;
                ActionType type = ActionType.INVESTIGATE;
                Role info = FindOutRole(action.PrimaryTarget);

                yield return new InvestigateDetail(type, executor, target, info);
            }
        }

        private Role FindOutRole(Role role)
        {
            if(role is GodfatherRole)
            {
                //Get random role from current setup of actions
                var executors = logs.Select(a => a.Executor);
                var targets = logs.Select(a => a.PrimaryTarget);
                var roles = executors.Union(targets);

                Random random = new Random();
                Role result = null;

                do
                {
                    result = roles.ElementAt(random.Next(roles.Count()));
                }
                while(result == role);

                return result;
            }
            else return role;
        }
    }
}