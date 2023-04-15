using System.Text.Json.Serialization;

namespace Net.Contexts.Night
{
    [Serializable]
    public class NightContext : SessionContext
    {
        public NightStep Step { get; }

        [JsonConstructor]
        public NightContext(NightStep step)
        {
            Step = step;
        }
    }

    public enum NightStep : byte
    {
        START_REMINDER,
        ALLOW_SELECTION,
        DISALLOW_SELECTION,
        END
    }
}