using System.Windows.Media;

namespace WPFApplication.Model
{
    public sealed class NonLynchPlayerState : DayPlayerState
    {
        public NonLynchPlayerState()
            : base(0UL, "Non-Lynch", true, Colors.White)
        {

        }
    }
}