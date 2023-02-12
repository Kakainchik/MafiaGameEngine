using Net.Contexts;
using Net.Models;
using Net.Providers;

namespace Net.Clients
{
    public interface IClient : IDisposable
    {
        Guid SessionId { get; }
        IProvider SessionProvider { get; }
        IProvider ChatProvider { get; }

        event EventHandler<bool>? Disconnected;
        event EventHandler<Context>? MessageIncomed;

        Task<ConnectValidation> ConnectAsync();
        Task<bool> RetryConnectAsync();
        void Disconnect();
        void SubmitMessage(Context context);
    }
}