using System.Timers;
using Timer = System.Timers.Timer;

namespace Net.Manager
{
    public class TimerFacade : ITimerFacade, IDisposable
    {
        private const string NO_STEPS_ERROR = "No steps are presented.";

        private LinkedList<TimerStruct> steps;
        private LinkedListNode<TimerStruct> pointer;
        private Timer stepTimer;
        private bool disposedValue;

        public TimerFacade(TimerStruct[] steps)
        {
            if(steps.Length == 0)
                throw new ArgumentOutOfRangeException(nameof(steps), NO_STEPS_ERROR);

            this.steps = new LinkedList<TimerStruct>(steps);
            pointer = this.steps.First!;
            stepTimer = new Timer();

            stepTimer.AutoReset = false;
            stepTimer.Elapsed += StepTimer_Elapsed;
        }

        #region ITimerFacade

        public void First()
        {
            pointer = steps.First!;

            stepTimer.Interval = pointer.Value.Milliseconds;
            stepTimer.Start();
        }

        /// <summary>
        /// <inheritdoc/>
        /// Has no effect if there is not next action in order.
        /// </summary>
        public void Next()
        {
            if(pointer.Next is null) return;
            pointer = pointer.Next;

            stepTimer.Interval = pointer.Value.Milliseconds;
            stepTimer.Start();
        }

        public void Exit()
        {
            pointer = steps.Last!;

            stepTimer.Interval = pointer.Value.Milliseconds;
            stepTimer.Start();
        }

        #endregion
#nullable disable warnings

        private void StepTimer_Elapsed(object? sender, ElapsedEventArgs e)
        {
            pointer.Value.Step.Invoke();
        }

        #region IDisposable

        protected virtual void Dispose(bool disposing)
        {
            if(!disposedValue)
            {
                if(disposing)
                {
                    stepTimer.Stop();
                    stepTimer.Dispose();
                }

                steps = null;
                pointer = null;
                stepTimer = null;

                disposedValue = true;
            }
        }

        public void Dispose()
        {
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }

#nullable restore warnings
        #endregion
    }
}