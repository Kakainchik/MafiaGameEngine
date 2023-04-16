using Net.Clients;
using Net.Contexts;
using Net.Contexts.Game;
using Net.Contexts.Intro;

namespace WPFApplication.ViewModel
{
    public class IntroGameClientViewModel : IntroGameViewModel
    {
        public IntroGameClientViewModel(IClient client) : base(client)
        {
            this.client.Disconnected += Client_Disconnected;
            this.client.MessageIncomed += Client_MessageIncomed;
        }

        public override void HandlePageChange(ChangeablePage page)
        {
            client.Disconnected -= Client_Disconnected;
            client.MessageIncomed -= Client_MessageIncomed;

            Successor?.AssertPage(page);
        }

        protected override void HandleIntroRunGame(IntroRunGameContext con)
        {
            var nextPage = new RunningGameClientViewModel(client, ownPlayer)
            {
                Successor = base.Successor
            };
            //Next to game page
            HandlePageChange(nextPage);
        }

        private void Client_MessageIncomed(object? sender, Context e)
        {
            switch(e)
            {
                case IntroContext con when con.Step == IntroStep.NAME_IN:
                {
                    HandleNameIn(con);
                    break;
                }
                case IntroContext con when con.Step == IntroStep.NAME_OUT:
                {
                    HandleNameOut();
                    break;
                }
                case IntroPlayerContext con:
                {
                    HandleIntroPlayer(con);
                    break;
                }
                case IntroContext con when con.Step == IntroStep.START:
                {
                    HandleStart();
                    break;
                }
                case IntroContext con when con.Step == IntroStep.MIDDLE:
                {
                    HandleMiddle();
                    break;
                }
                case IntroContext con when con.Step == IntroStep.END:
                {
                    HandleEnd();
                    break;
                }
                case IntroContext con when con.Step == IntroStep.TIP:
                {
                    HandleTip();
                    break;
                }
                case CommonPlayerStateContext con:
                {
                    HandleCommonPlayerState(con);
                    break;
                }
                case IntroRunGameContext con:
                {
                    HandleIntroRunGame(con);
                    break;
                }
                default:
                    break;
            }
        }

        private async void Client_Disconnected(object? sender, bool e)
        {
            if(e) AbortConnections();
            else await client.RetryConnectAsync();
        }
    }
}