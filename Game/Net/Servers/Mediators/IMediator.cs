using Net.Contexts;

namespace Net.Servers.Mediators
{
    public interface IMediator : IDisposable
    {
        void Accept(Context message);
    }
}