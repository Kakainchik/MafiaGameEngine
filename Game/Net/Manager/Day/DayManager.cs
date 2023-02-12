using GameLogic;
using GameLogic.Cycles;
using Net.Servers;
using Timer = System.Timers.Timer;

namespace Net.Manager.Day
{
    public abstract class DayManager : Manager
    {
        protected readonly TimeSpan SHORT_INTERVAL = new TimeSpan(0, 0, 0, 6, 50);
        protected readonly TimeSpan AVERAGE_INTERVAL = new TimeSpan(0, 0, 0, 20, 50);
        protected readonly TimeSpan LONG_INTERVAL = new TimeSpan(0, 0, 0, 35, 50);

        protected IEnumerable<Player> players;
        protected Timer electionTimer;
        protected Game game;

        protected DayCycle Cycle => (DayCycle)game.CurrentCycle;

        public DayManager(LANServer server, Game game, IEnumerable<Player> players)
            : base(server)
        {
            this.game = game;
            this.players = players;
            electionTimer = new Timer();
            electionTimer.AutoReset = false;
        }

        public abstract void StartDay();
        public abstract void SendVoteFromTo(Guid voterId, Guid targetId);
        public abstract void SendVoteForNonLynch(Guid voterId);
        public abstract void Unvote(Guid voterId);
    }
}