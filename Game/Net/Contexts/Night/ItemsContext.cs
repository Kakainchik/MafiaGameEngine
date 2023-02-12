namespace Net.Contexts.Night
{
    [Serializable]
    public class ItemsContext : SessionContext
    {
        public int Items { get; }

        public ItemsContext(int items)
        {
            Items = items;
        }
    }
}