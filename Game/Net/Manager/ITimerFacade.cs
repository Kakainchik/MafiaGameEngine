namespace Net.Manager
{
    public interface ITimerFacade : IDisposable
    {
        /// <summary>
        /// Put pointer to the start of actions list and start timer to execute it.
        /// </summary>
        void First();

        /// <summary>
        /// Put pointer to the next action and start timer to execute it.
        /// </summary>
        void Next();

        /// <summary>
        /// Put pointer to the end of actions list and start timer to execute it.
        /// </summary>
        void Exit();
    }
}