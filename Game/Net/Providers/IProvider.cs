using Net.Contexts;

namespace Net.Providers
{
    public interface IProvider : IDisposable
    {
        Task ConnectAsync();
        Task SendMessageAsync(Context message);
        Task SendPrivateMessageAsync(Context message, ulong receiver);
        Task InformServerAsync(Context message);
        void Close(bool suppressCallback);
    }
}