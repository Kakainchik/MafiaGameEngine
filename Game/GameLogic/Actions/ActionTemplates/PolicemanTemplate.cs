using GameLogic.Model;
using GameLogic.Roles;

namespace GameLogic.Actions.ActionTemplates
{
    public class PolicemanTemplate : ActionTemplate
    {
        public PolicemanTemplate(IEnumerable<ActionLog> logs) : base(logs)
        {

        }

        public override IEnumerable<BaseDetail> Convert()
        {
            //Find all policemans with success
            var policeman = logs.Where(s => s.Executor is PolicemanRole)
                .Where(s => s.Success);

            foreach(var action in policeman)
            {
                ActionType type = ActionType.DETECT;
                Player executor = action.Executor.Owner;
                Player target = action.PrimaryTarget.Owner;
                DetectInfo info = FindOutTeam(action.PrimaryTarget);

                yield return new DetectDetail(type, executor, target, info);
            }
        }

        private DetectInfo FindOutTeam(Role role)
        {
            switch(role)
            {
                case CitizenRole _:
                    return DetectInfo.PEACEFUL;
                case GodfatherRole _:
                    //Godfather has immune to detection
                    return DetectInfo.PEACEFUL;
                case CursedRole _:
                    return DetectInfo.PEACEFUL;
                case DetectiveRole _:
                    return DetectInfo.PEACEFUL;
                case DoctorRole _:
                    return DetectInfo.PEACEFUL;
                case DriverRole _:
                    return DetectInfo.PEACEFUL;
                case MasonRole _:
                    return DetectInfo.PEACEFUL;
                case PolicemanRole _:
                    return DetectInfo.PEACEFUL;
                case ProstituteRole _:
                    return DetectInfo.PEACEFUL;
                case RecruitRole _:
                    return DetectInfo.PEACEFUL;
                case VigilanteRole _:
                    return DetectInfo.PEACEFUL;
                default:
                    return DetectInfo.DANGEROUS;
            }
        }
    }

    public enum DetectInfo : byte
    {
        PEACEFUL,
        DANGEROUS
    }
}