namespace GameLogic.Cycles.History
{
    public class GameHistory
    {
        internal CycleMemento tempCycle;

        public Stack<CycleMemento> History { get; private set; }

        public GameHistory()
        {
            tempCycle = new CycleMemento();
            History = new Stack<CycleMemento>();
        }

        public void SetTurn(int turn) => tempCycle.CopyTurn(turn);

        public void SaveDay(DayMemento memento) => tempCycle.CopyDay(memento);

        public void SaveLynch(LynchMemento memento) => tempCycle.CopyLynch(memento);

        public void SaveNight(NightMemento memento) => tempCycle.CopyNight(memento);

        public void SaveMorning(MorningMemento memento) => tempCycle.CopyMorning(memento);

        public void MakeTurnBackup()
        {
            History.Push(tempCycle);
            //Clear temp
            tempCycle = new CycleMemento();
        }
    }
}