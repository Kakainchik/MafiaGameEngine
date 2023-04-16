using Net.Contexts;
using Net.Models;
using Net.Providers;

namespace Net.Clients
{
    public interface IClient : IDisposable
    {
        ulong ClientId { get; }
        IProvider SessionProvider { get; }
        IProvider ChatProvider { get; }

        event EventHandler<bool>? Disconnected;
        event EventHandler<Context>? MessageIncomed;

        Task<ConnectValidation> ConnectAndAuthorizeAsync();
        Task<bool> RetryConnectAsync();
        void Disconnect();
        void SubmitMessage(Context context);
    }
}