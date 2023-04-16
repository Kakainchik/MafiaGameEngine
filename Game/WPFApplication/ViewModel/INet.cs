namespace WPFApplication.ViewModel
{
    public interface INetHolder
    {
        void AbortConnections();
    }

    public interface INetUser
    {
        INetHolder? NetHolder { get; }
    }
}