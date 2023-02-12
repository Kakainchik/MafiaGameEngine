using GameLogic;
using Net.Servers;

namespace Net.Manager.Day
{
    public class DayFactory : IFactory<DayManager>
    {
        private LANServer server;
        private Game game;
        private IEnumerable<Player> players;

        public DayFactory(LANServer server,
            Game game,
            IEnumerable<Player> players)
        {
            this.server = server;
            this.game = game;
            this.players = players;
        }

        public DayManager Create()
        {
            return new MajorityDayManager(server, game, players);
        }
    }
}