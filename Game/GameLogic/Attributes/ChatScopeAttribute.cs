namespace GameLogic.Attributes
{
    [AttributeUsage(AttributeTargets.Class, Inherited = false, AllowMultiple = true)]
    public sealed class ChatScopeAttribute : Attribute
    {
        private readonly ChatScope scope;

        public ChatScope Scope => scope;

        public bool CanWrite { get; set; }

        public ChatScopeAttribute(ChatScope scope)
        {
            this.scope = scope;
        }
    }

    public enum ChatScope : byte
    {
        MAFIA,
        MASON,
        CULTUS,
        DEAD,
        UNDEAD,
        GENERAL_ALIVE
    }
}