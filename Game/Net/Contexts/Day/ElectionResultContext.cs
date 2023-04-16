using System.Text.Json.Serialization;

namespace Net.Contexts.Day
{
    [Serializable]
    public class ElectionResultContext : SessionContext
    {
        public ulong? ElectedId { get; }

        [JsonConstructor]
        public ElectionResultContext(ulong? electedId)
        {
            ElectedId = electedId;
        }
    }
}