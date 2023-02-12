namespace Net.Contexts.Day
{
    [Serializable]
    public class DayContext : SessionContext
    {
        public DayStep Step { get; }
        public int Number { get; }

        public DayContext(DayStep step, int number)
        {
            Step = step;
            Number = number;
        }

        public DayContext(DayStep step) : this(step, 0)
        {

        }
    }

    public enum DayStep : byte
    {
        START_DAY,
        START_BALLOT,
        END_BALLOT,
        FIRST_DAY_CASE
    }
}