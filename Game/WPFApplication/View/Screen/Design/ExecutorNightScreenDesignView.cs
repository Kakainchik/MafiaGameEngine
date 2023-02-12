using System;
using System.Collections.ObjectModel;
using System.Windows.Media;
using WPFApplication.Model;
using WPFApplication.ViewModel;

namespace WPFApplication.View.Design
{
    internal class ExecutorNightScreenDesignView : TNightScreenState
    {
        public ExecutorNightScreenDesignView() : base(null, RoleVisual.CITIZEN)
        {
            base.PlayersState = new ObservableCollection<NightPlayerState>()
            {
                new NightPlayerState(Guid.NewGuid(), "Alice", true, Colors.Pink, false),
                new NightPlayerState(Guid.NewGuid(), "Bob", true, Colors.Plum, false) { IsPicked = true },
                new NightPlayerState(Guid.NewGuid(), "Clark", true, Colors.Green, false),
                new NightPlayerState(Guid.NewGuid(), "Dad", false, Colors.Yellow, false),
                new NightPlayerState(Guid.NewGuid(), "Enigma", true, Colors.Blue, false)
            };
        }
    }
}