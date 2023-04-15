using System.Text.Json.Serialization;

namespace Net.Contexts.Night
{
    [Serializable]
    public class ItemsContext : SessionContext
    {
        public int Items { get; }

        [JsonConstructor]
        public ItemsContext(int items)
        {
            Items = items;
        }
    }
}