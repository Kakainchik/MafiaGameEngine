using System.Collections.ObjectModel;
using System.Windows.Media;
using WPFApplication.Model;
using WPFApplication.ViewModel;

#pragma warning disable CS8625 // Cannot convert null literal to non-nullable reference type.

namespace WPFApplication.View.Design
{
    internal class ExecutorNightScreenDesignView : TNightScreenState
    {
        public ExecutorNightScreenDesignView() : base(null, RoleVisual.CITIZEN)
        {
            base.PlayersState = new ObservableCollection<NightPlayerState>()
            {
                new NightPlayerState(1UL, "Alice", true, Colors.Pink, false),
                new NightPlayerState(2UL, "Bob", true, Colors.Plum, false) { IsPicked = true },
                new NightPlayerState(3UL, "Clark", true, Colors.Green, false),
                new NightPlayerState(4UL, "Dad", false, Colors.Yellow, false),
                new NightPlayerState(5UL, "Enigma", true, Colors.Blue, false)
            };
        }
    }
}