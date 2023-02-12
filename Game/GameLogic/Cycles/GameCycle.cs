namespace GameLogic.Cycles
{
    public abstract class GameCycle
    {
        protected List<Player> alivePlayers;

        public GameCycle(List<Player> alivePlayers)
        {
            this.alivePlayers = alivePlayers;
        }
    }
}