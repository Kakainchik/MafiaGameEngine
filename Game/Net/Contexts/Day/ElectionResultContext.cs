using System.Text.Json.Serialization;

namespace Net.Contexts.Day
{
    [Serializable]
    public class ElectionResultContext : SessionContext
    {
        public Guid ElectedId { get; }

        [JsonConstructor]
        public ElectionResultContext(Guid electedId)
        {
            ElectedId = electedId;
        }
    }
}