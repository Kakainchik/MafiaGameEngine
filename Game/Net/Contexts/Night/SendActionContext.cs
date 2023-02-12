namespace Net.Contexts.Night
{
    [Serializable]
    public class SendActionContext : SessionContext
    {
        public Guid PrimaryTarget { get; }
        public bool HasNonExFlag { get; }

        public SendActionContext(Guid primary)
        {
            PrimaryTarget = primary;
        }

        public SendActionContext()
        {
            HasNonExFlag = true;
        }
    }
}