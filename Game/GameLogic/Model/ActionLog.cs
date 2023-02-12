using GameLogic.Roles;

namespace GameLogic.Model
{
    public class ActionLog
    {
        public Role Executor { get; set; }
        public Role PrimaryTarget { get; set; }
        public bool Success { get; set; }
        public bool IsBlocked { get; set; }

        public static LogBuilder Builder => new LogBuilder();
    }
}