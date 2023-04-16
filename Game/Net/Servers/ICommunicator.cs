using Net.Contexts;
using Net.Servers.Units;

namespace Net.Servers
{
    public interface ISessionCommunicator
    {
        void SendSessionMessage(Context message, ulong receiver);
        void AcceptSystemSessionMessage(Context message);
        void BroadcastSessionMessage(Context message);
        void BroadcastSessionMessage(Context message, ulong instead);
        void AbortConnection(ulong clientId);
    }

    public interface IChatCommunicator
    {
        void SendChatMessage(Context message, ulong receiver);
        void AcceptSystemChatMessage(Context message);
        void BroadcastChatMessage(Context message);
        void BroadcastChatMessage(Context message, ulong instead);
        void AttachChat(ChatUnit unit);
        void DetachChat(ChatUnit unit);
    }
}