using GameLogic.Roles;

namespace GameLogic
{
    public class KillEventArgs
    {
        public static KillEventArgs Empty = new KillEventArgs(null);

        public Role Killer { get; }

        public KillEventArgs(Role killer)
        {
            this.Killer = killer;
        }
    }
}