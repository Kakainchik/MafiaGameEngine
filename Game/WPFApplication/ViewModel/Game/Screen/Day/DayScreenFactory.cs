using Net.Clients;
using WPFApplication.Model;

namespace WPFApplication.ViewModel
{
    public class DayScreenFactory : IScreenFactory
    {
        private IClient client;

        public DayScreenFactory(IClient client)
        {
            this.client = client;
        }

        public ScreenState Create(RoleVisual role, bool isAlive)
        {
            if(!isAlive) return new DeadDayScreenState(client);
            else return new AliveDayScreenState(client);
        }
    }
}