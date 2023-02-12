namespace GameLogic.Cycles.History
{
    public struct CycleMemento
    {
        internal int turn;
        internal DayMemento day;
        internal LynchMemento lynch;
        internal NightMemento night;
        internal MorningMemento morning;

        public int Turn => turn;
        public DayMemento Day => day;
        public LynchMemento Lynch => lynch;
        public NightMemento Night => night;
        public MorningMemento Morning => morning;

        internal void CopyTurn(int turn) => this.turn = turn;
        internal void CopyDay(DayMemento memento) => day = memento;
        internal void CopyLynch(LynchMemento memento) => lynch = memento;
        internal void CopyNight(NightMemento memento) => night = memento;
        internal void CopyMorning(MorningMemento memento) => morning = memento;
    }
}