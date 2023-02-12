using Net.Clients;
using Net.Contexts;
using Net.Contexts.Chat;
using Net.Contexts.Game;
using Net.Servers.Mediators;
using WPFApplication.Extensions;
using WPFApplication.Model.PlayerData;

namespace WPFApplication.ViewModel
{
    public class RunningGameHostViewModel : RunningGameViewModel
    {
        private GameRouterMediator mediator;

        public RunningGameHostViewModel(IClient client,
            GameRouterMediator mediator,
            CommonPlayerState ownPlayer)
            : base(client, ownPlayer)
        {
            this.mediator = mediator;

            this.client.Disconnected += Client_Disconnected;
            this.client.MessageIncomed += Client_MessageIncomed;

            this.mediator.StartCycles();
        }

        public override void HandlePageChange(ChangeablePage page)
        {
            client.Disconnected -= Client_Disconnected;
            client.MessageIncomed -= Client_MessageIncomed;

            Successor?.AssertPage(page);
        }

        private async void Client_Disconnected(object sender, bool e)
        {
            if(e) NetHolder?.AbortConnections();
            else await client.RetryConnectAsync();
        }

        private void Client_MessageIncomed(object sender, Context e)
        {
            switch(e)
            {
                case ScreenContext con:
                {
                    ResolveScreen(con);
                    break;
                }
                case ScopedMessageContext con:
                {
                    TransmitScopedMessage(con);
                    break;
                }
                case CommonPlayerStateContext con:
                {
                    //Save new info of own player state
                    ownPlayer.Role = con.Role.MapRole();
                    ownPlayer.IsAlive = con.IsAlive;
                    break;
                }
                default:
                {
                    mainScreen.HandleMessage(e);
                    break;
                }
            }
        }
    }
}