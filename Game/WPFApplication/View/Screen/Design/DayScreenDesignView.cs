using System;
using System.Collections.ObjectModel;
using System.Windows.Media;
using WPFApplication.Model;
using WPFApplication.ViewModel;

namespace WPFApplication.View.Design
{
    internal class DayScreenDesignView : DayScreenState
    {
        public DayScreenDesignView() : base(null)
        {
            base.PlayersState = new ObservableCollection<DayPlayerState>()
            {
                new DayPlayerState(Guid.NewGuid(), "Alice", true, Colors.Pink),
                new DayPlayerState(Guid.NewGuid(), "Bob", true, Colors.Plum),
                new DayPlayerState(Guid.NewGuid(), "Clark", true, Colors.Green),
                new DayPlayerState(Guid.NewGuid(), "Dad", false, Colors.Yellow),
                new DayPlayerState(Guid.NewGuid(), "Enigma", true, Colors.Blue)
            };

            PlayersState[0].Vote.OwnVotes = 2;

            PlayersState[1].Vote.VoteTargetNickname = PlayersState[0].Details.Nickname;
            PlayersState[1].Vote.TColor = PlayersState[0].Details.NColor;

            PlayersState[2].Vote.VoteTargetNickname = PlayersState[0].Details.Nickname;
            PlayersState[2].Vote.TColor = PlayersState[0].Details.NColor;
        }
    }
}