namespace Net.Manager
{
    public struct TimerStruct
    {
        private Action step;
        private double milliseconds;

        public Action Step => step;
        public double Milliseconds => milliseconds;

        public TimerStruct(double milliseconds, Action step)
        {
            this.milliseconds = milliseconds;
            this.step = step;
        }
    }
}