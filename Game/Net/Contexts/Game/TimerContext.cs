using System.Text.Json.Serialization;

namespace Net.Contexts.Game
{
    [Serializable]
    public class TimerContext : SessionContext
    {
        public TimeSpan InitialTime { get; }
        public bool ToStart { get; }

        [JsonConstructor]
        public TimerContext(TimeSpan initialTime, bool toStart)
        {
            InitialTime = initialTime;
            ToStart = toStart;
        }
    }
}