using Net.Contexts;
using Net.Servers.Units;

namespace Net.Servers
{
    public interface ISessionCommunicator
    {
        void SendSessionMessage(Context message, Guid receiver);
        void AcceptSystemSessionMessage(Context message);
        void BroadcastSessionMessage(Context message);
        void BroadcastSessionMessage(Context message, Guid instead);
        void AbortConnection(Guid sessionId);
    }

    public interface IChatCommunicator
    {
        void SendChatMessage(Context message, Guid receiver);
        void AcceptSystemChatMessage(Context message);
        void BroadcastChatMessage(Context message);
        void BroadcastChatMessage(Context message, Guid instead);
        void AttachChat(ChatUnit unit);
        void DetachChat(ChatUnit unit);
    }
}