using Net.Servers;

namespace Net.Manager.Intro
{
    public class IntroFactory : IFactory<IntroManager>
    {
        private LANServer server;

        public IntroFactory(LANServer server)
        {
            this.server = server;
        }

        public IntroManager Create()
        {
            return new GeneralIntroManager(server);
        }
    }
}