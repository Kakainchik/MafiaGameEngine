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

        public IntroContext(IntroStep step, string city) : this(step)
        {
            CityName = city;
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