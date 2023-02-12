using Net.Servers;

namespace Net.Manager.Intro
{
    public abstract class IntroManager : Manager
    {
        protected readonly TimeSpan SHORT_INTERVAL = new TimeSpan(0, 0, 0, 4, 50);
        protected readonly TimeSpan AVERAGE_INTERVAL = new TimeSpan(0, 0, 0, 11, 50);
        protected readonly TimeSpan LONG_INTERVAL = new TimeSpan(0, 0, 0, 15, 50);

        public IntroManager(LANServer server)
            : base(server)
        {

        }

        public abstract void Initialize(string city);
        public abstract void StartIntroStep();
    }
}