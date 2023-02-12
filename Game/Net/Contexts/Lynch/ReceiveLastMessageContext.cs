namespace Net.Contexts.Lynch
{
    [Serializable]
    public class ReceiveLastMessageContext : SessionContext
    {
        public string LastMessage { get; }

        public ReceiveLastMessageContext(string message)
        {
            LastMessage = message;
        }
    }
}