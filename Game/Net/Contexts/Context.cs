using System.Text.Json.Serialization;

namespace Net.Contexts
{
    [Serializable]
    [JsonDerivedType(typeof(SessionContext))]
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
        public bool IsPrivate { get; internal set; }
        public bool IsForServer { get; internal set; }
        public Guid Receiver { get; internal set; }
        public Guid Sender { get; internal set; }
    }
}