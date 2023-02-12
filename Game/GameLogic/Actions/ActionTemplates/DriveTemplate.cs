using GameLogic.Model;
using GameLogic.Roles;

namespace GameLogic.Actions.ActionTemplates
{
    public class DriveTemplate : ActionTemplate
    {
        public DriveTemplate(IEnumerable<ActionLog> logs) : base(logs)
        {

        }

        public override IEnumerable<BaseDetail> Convert()
        {
            var driver = logs.Where(s => s.Executor is DriverRole);

            foreach(DoubleActionLog action in driver)
            {
                Player primary = action.PrimaryTarget.Owner;
                Player secondary = action.SecondaryTarget.Owner;
                Player executor = action.Executor.Owner;
                ActionType type = ActionType.DRIVER_SWAP;

                BaseDetail detail = new BaseDetail(type, executor, primary);
                yield return new DoubleDetail(detail, secondary);
            }
        }
    }
}