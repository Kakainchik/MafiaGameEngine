using GameLogic.Cycles.History;
using GameLogic.Interfaces;

namespace GameLogic.Cycles
{
    public class LynchCycle : GameCycle
    {
        private Player elected;

        public LynchCycle(List<Player> alivePlayers, IVotable elected) : base(alivePlayers)
        {
            this.elected = elected as Player;

            if(elected == null)
                throw new Exception("Lynch cycle cannot begin with no elected player.");
        }

        internal event EventHandler<LynchMemento> LynchEnded;

        public Player GetElected()
        {
            return elected;
        }

        public void Lynch(string message)
        {
            var match = alivePlayers.First(p => p.Id.Equals(elected.Id));

            match.LastMessage = message;
            match.Lynch();

            LynchMemento memento = new LynchMemento
            {
                lynchedPlayer = match,
                lastMessage = message
            };

            LynchEnded?.Invoke(this, memento);
        }
    }
}