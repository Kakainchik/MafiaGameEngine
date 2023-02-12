using GameLogic.Attributes;
using Net.Models;

namespace Net.Contexts.Chat
{
    [Serializable]
    public class ScopedMessageContext : MessageContext
    {
        public ChatScope Scope { get; }
        public RGB NColor { get; }

        public ScopedMessageContext(string senderName,
            string message,
            ChatScope scope,
            RGB color)
            : base(senderName, message)
        {
            Scope = scope;
            NColor = color;
        }
    }
}