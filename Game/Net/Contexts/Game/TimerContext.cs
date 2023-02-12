namespace Net.Contexts.Game
{
    [Serializable]
    public class TimerContext : SessionContext
    {
        public TimeSpan InitialTime { get; }
        public bool ToStart { get; }

        public TimerContext(TimeSpan initial, bool toStart)
        {
            InitialTime = initial;
            ToStart = toStart;
        }
    }
}