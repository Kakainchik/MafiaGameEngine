using GameLogic;
using Net.Models;
using Net.Servers;

namespace Net.Manager.Lynch
{
    public class LynchFactory : IFactory<LynchManager>
    {
        private LANServer server;
        private Game game;
        private IDictionary<Player, SessionPLayer> players;

        public LynchFactory(LANServer server,
            Game game,
            IDictionary<Player, SessionPLayer> players)
        {
            this.server = server;
            this.game = game;
            this.players = players;
        }

        public LynchManager Create()
        {
            return new LynchManager(server, game, players);
        }
    }
}