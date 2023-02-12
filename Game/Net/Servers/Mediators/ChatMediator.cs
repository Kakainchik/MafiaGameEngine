using Net.Contexts;
using Net.Contexts.Connection;

namespace Net.Servers.Mediators
{
    public class ChatMediator : IMediator
    {
        public LANServer Holder { get; }

        public ChatMediator(LANServer server)
        {
            Holder = server;
        }

        public void Accept(Context context)
        {
            switch(context)
            {
                case SessionIdContext con:
                {

                    break;
                }
            }
        }
    }
}