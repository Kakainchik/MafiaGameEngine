using GameLogic.Interfaces;

namespace GameLogic.Cycles.History
{
    public struct DayMemento
    {
        internal IVotable electedPlayer;
        internal int gotVotes;

        public IVotable ElectedPlayer => electedPlayer;
        public int GotVotes => gotVotes;
    }
}