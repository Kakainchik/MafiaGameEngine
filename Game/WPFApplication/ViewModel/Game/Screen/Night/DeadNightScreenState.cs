using Net.Clients;
using Net.Contexts.Night;
using System.Threading.Tasks;
using System.Windows.Documents;
using WPFApplication.Model;
using WPFApplication.Resources.GameStoryText;

namespace WPFApplication.ViewModel
{
    public class DeadNightScreenState : NightScreenState
    {
        public DeadNightScreenState(IClient client, RoleVisual role) : base(client, role)
        {

        }

        protected override void HandleStartReminder()
        {
            //Plug
            StoryRun(new Run(NightResources.NightStart));
        }

        protected override void HandleDissalowSelection()
        {
            //Plug
            //Send non executable flag
            var message = new SendActionContext();
            _ = client.SessionProvider.InformServerAsync(message);
        }
    }
}