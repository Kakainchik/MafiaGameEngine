namespace Net.Contexts.Lynch
{
    [Serializable]
    public class SendLastMessageContext : SessionContext
    {
        public string LastMessage { get; }

        public SendLastMessageContext(string message)
        {
            LastMessage = message;
        }
    }
}