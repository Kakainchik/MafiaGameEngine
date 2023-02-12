using GameLogic.Model;
using System.Collections.Generic;

namespace GameLogic.Cycles.History
{
    public struct NightMemento
    {
        internal IEnumerable<ActionLog> revealedActions;
        internal IEnumerable<Player> nightPlayer;

        public IEnumerable<ActionLog> RevealedActions => revealedActions;
    }
}