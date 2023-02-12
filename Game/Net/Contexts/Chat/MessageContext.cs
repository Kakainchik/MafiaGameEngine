namespace Net.Contexts.Chat
{
    [Serializable]
    public class MessageContext : ChatContext
    {
        public string SenderName { get; }
        public string Message { get; }

        public MessageContext(string senderName, string message)
        {
            SenderName = senderName;
            Message = message;
        }
    }
}