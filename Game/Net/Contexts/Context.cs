namespace Net.Contexts
{
    [Serializable]
    public abstract class Context
    {
        internal ContextHeader Header;

        public ContextPresenter Presenter;
    }

    [Serializable]
    public struct ContextHeader
    {
        public int Length { get; internal set; }
    }

    [Serializable]
    public struct ContextPresenter
    {
        public bool IsPrivate { get; set; }
        public bool IsForServer { get; set; }
        public ulong Receiver { get; set; }
        public ulong Sender { get; set; }
    }
}