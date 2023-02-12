using GameLogic.Interfaces;

namespace GameLogic.Cycles.History
{
    public struct LynchMemento
    {
        internal ILynch lynchedPlayer;
        internal string lastMessage;

        public ILynch LynchedPlayer => lynchedPlayer;
        public string LastMessage => lastMessage;
    }
}