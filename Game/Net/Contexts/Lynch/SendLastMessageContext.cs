using System.Text.Json.Serialization;

namespace Net.Contexts.Lynch
{
    [Serializable]
    public class SendLastMessageContext : SessionContext
    {
        public string LastMessage { get; }

        [JsonConstructor]
        public SendLastMessageContext(string lastMessage)
        {
            LastMessage = lastMessage;
        }
    }
}