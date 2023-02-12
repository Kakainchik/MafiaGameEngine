using Net.Clients;
using Net.Contexts.Night;
using System.Threading.Tasks;
using WPFApplication.Model;

namespace WPFApplication.ViewModel
{
    public class NonNightScreenState : NightScreenState
    {
        public NonNightScreenState(IClient client, RoleVisual role) : base(client, role)
        {

        }

        protected override void HandleDissalowSelection()
        {
            //Send non executable flag
            var message = new SendActionContext();
            _ = client.SessionProvider.InformServerAsync(message);
        }
    }
}