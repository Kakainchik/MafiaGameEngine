using Net.Clients;
using WPFApplication.Model;

namespace WPFApplication.ViewModel
{
    public class LynchScreenFactory : IScreenFactory
    {
        private IClient client;

        public LynchScreenFactory(IClient client)
        {
            this.client = client;
        }

        public ScreenState Create(RoleVisual role, bool isAlive)
        {
            return new LynchScreenState(client);
        }
    }
}