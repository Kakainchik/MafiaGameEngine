using GameLogic.Roles;

namespace GameLogic.Model
{
    public class DoubleActionLog : ActionLog
    {
        public Role SecondaryTarget { get; set; }
    }
}