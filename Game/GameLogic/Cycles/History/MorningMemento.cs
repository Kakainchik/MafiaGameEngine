using GameLogic.Model;
using System.Collections.Generic;

namespace GameLogic.Cycles.History
{
    public struct MorningMemento
    {
        internal IEnumerable<DeathDetail> deaths;

        public IEnumerable<DeathDetail> Deaths => deaths;
    }
}