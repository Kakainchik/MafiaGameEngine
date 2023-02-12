using Net.Contexts;

namespace Net.Providers
{
    public interface IProvider : IDisposable
    {
        Task ConnectAsync();
        Task SendMessageAsync(Context message);
        Task SendPrivateMessageAsync(Context message, Guid receiver);
        Task InformServerAsync(Context message);
        void Close(bool suppressCallback);
    }
}