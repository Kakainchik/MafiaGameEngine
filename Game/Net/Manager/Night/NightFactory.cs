using GameLogic;
using Net.Servers;

namespace Net.Manager.Night
{
    public class NightFactory : IFactory<NightManager>
    {
        private LANServer server;
        private Game game;
        private IEnumerable<Player> players;

        public NightFactory(LANServer server, Game game, IEnumerable<Player> players)
        {
            this.server = server;
            this.game = game;
            this.players = players;
        }

        public NightManager Create()
        {
            return new NightManager(server, game, players);
        }
    }
}