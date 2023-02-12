namespace Net.Contexts.Night
{
    [Serializable]
    public class SendDActionContext : SendActionContext
    {
        public Guid SecondaryTarget { get; }

        public SendDActionContext(Guid primary, Guid secondary)
            : base(primary)
        {
            SecondaryTarget = secondary;
        }
    }
}