using System.Text.Json.Serialization;

namespace Net.Contexts.Intro
{
    [Serializable]
    public class IntroContext : SessionContext
    {
        public IntroStep Step { get; }
        public string? CityName { get; }

        public IntroContext(IntroStep step)
        {
            Step = step;
        }

        [JsonConstructor]
        public IntroContext(IntroStep step, string cityName) : this(step)
        {
            CityName = cityName;
        }
    }

    public enum IntroStep : byte
    {
        START,
        NAME_IN,
        NAME_OUT,
        MIDDLE,
        END,
        TIP
    }
}