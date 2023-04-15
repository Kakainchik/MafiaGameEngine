using GameLogic.Attributes;
using Net.Models;
using System.Text.Json.Serialization;

namespace Net.Contexts.Chat
{
    [Serializable]
    public class ScopedMessageContext : MessageContext
    {
        public ChatScope Scope { get; }
        public RGB NColor { get; }

        [JsonConstructor]
        public ScopedMessageContext(string senderName,
            string message,
            ChatScope scope,
            RGB nColor)
            : base(senderName, message)
        {
            Scope = scope;
            NColor = nColor;
        }
    }
}