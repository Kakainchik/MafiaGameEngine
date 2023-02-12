using Net.Contexts;

namespace Net.Servers.Mediators
{
    public interface IMediator
    {
        void Accept(Context message);
    }
}