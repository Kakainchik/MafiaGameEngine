using GameLogic;
using Net.Models;
using Net.Servers;

namespace Net.Manager.Morning
{
    public class MorningFactory : IFactory<MorningManager>
    {
        private LANServer server;
        private Game game;
        private IDictionary<Player, SessionPLayer> players;

        public MorningFactory(LANServer server,
            Game game,
            IDictionary<Player, SessionPLayer> players)
        {
            this.server = server;
            this.game = game;
            this.players = players;
        }

        public MorningManager Create()
        {
            return new MorningManager(server, game, players);
        }
    }
}