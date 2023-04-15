using System.Text.Json.Serialization;

namespace Net.Contexts.Lynch
{
    [Serializable]
    public class ReceiveLastMessageContext : SessionContext
    {
        public string LastMessage { get; }

        [JsonConstructor]
        public ReceiveLastMessageContext(string lastMessage)
        {
            LastMessage = lastMessage;
        }
    }
}