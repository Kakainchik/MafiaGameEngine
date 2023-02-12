using GameLogic.Model;
using GameLogic.Roles;

namespace GameLogic.Actions.ActionTemplates
{
    public class RecruitTemplate : ActionTemplate
    {
        public RecruitTemplate(IEnumerable<ActionLog> logs) : base(logs)
        {

        }

        public override IEnumerable<BaseDetail> Convert()
        {
            //Find all recruiters with non-blocking
            var recruit = logs.Where(
                s => s.Executor is CultusLeaderRole
                || s.Executor is GodfatherRole)
                .Where(s => !s.IsBlocked);

            foreach(var action in recruit)
            {
                Player target = action.PrimaryTarget.Owner;
                Player executor = action.Executor.Owner;
                ActionType type = FindOutType(action.Executor);
                RecruitInfo info;
                if(action.Success)
                {
                    info = RecruitInfo.ADD_NEW_MEMBER;
                }
                else
                {
                    //Show that that role could not be recruited
                    info = RecruitInfo.NON_RECRUITABLE;
                }

                yield return new RecruitDetail(type, executor, target, info);
            }
        }

        private ActionType FindOutType(Role role)
        {
            switch(role)
            {
                case GodfatherRole _:
                    return ActionType.GODFATHER_RECRUIT;
                case CultusLeaderRole _:
                    return ActionType.CULTUS_LEADER_RECRUIT;
                default:
                    throw new Exception("There is not recruiter.");
            }
        }
    }

    public enum RecruitInfo : byte
    {
        ADD_NEW_MEMBER,
        NON_RECRUITABLE
    }
}