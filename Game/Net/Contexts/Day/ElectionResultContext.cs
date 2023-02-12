namespace Net.Contexts.Day
{
    [Serializable]
    public class ElectionResultContext : SessionContext
    {
        public Guid ElectedId { get; }

        public ElectionResultContext(Guid id)
        {
            ElectedId = id;
        }
    }
}