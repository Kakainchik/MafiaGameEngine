using Net.Clients;
using WPFApplication.Model;

namespace WPFApplication.ViewModel
{
    public class MorningScreenFactory : IScreenFactory
    {
        private IClient client;

        public MorningScreenFactory(IClient client)
        {
            this.client = client;
        }

        public ScreenState Create(RoleVisual role, bool isAlive)
        {
            return new MorningScreenState(client);
        }
    }
}