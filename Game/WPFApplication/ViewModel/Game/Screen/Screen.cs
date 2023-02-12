using Net.Contexts;
using WPFApplication.Core;

namespace WPFApplication.ViewModel
{
    public class Screen : ObservableObject
    {
        private ScreenState state;

        public ScreenState State
        {
            get => state;
            set
            {
                state = value;
                OnPropertyChanged(nameof(State));
            }
        }

        public Screen(ScreenState state)
        {
            State = state;
        }

        public void HandleMessage(Context context)
        {
            State.HandleContext(context);
        }
    }
}