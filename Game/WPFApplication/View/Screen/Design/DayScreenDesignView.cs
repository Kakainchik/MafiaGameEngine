using System.Collections.ObjectModel;
using System.Windows.Media;
using WPFApplication.Model;
using WPFApplication.ViewModel;

#pragma warning disable CS8625

namespace WPFApplication.View.Design
{
    internal class DayScreenDesignView : DayScreenState
    {
        public DayScreenDesignView() : base(null)
        {
            base.PlayersState = new ObservableCollection<DayPlayerState>()
            {
                new DayPlayerState(1UL, "Alice", true, Colors.Pink),
                new DayPlayerState(2UL, "Bob", true, Colors.Plum),
                new DayPlayerState(3UL, "Clark", true, Colors.Green),
                new DayPlayerState(4UL, "Dad", false, Colors.Yellow),
                new DayPlayerState(5UL, "Enigma", true, Colors.Blue)
            };

            PlayersState[0].Vote.OwnVotes = 2;

            PlayersState[1].Vote.VoteTargetNickname = PlayersState[0].Details.Nickname;
            PlayersState[1].Vote.TColor = PlayersState[0].Details.NColor;

            PlayersState[2].Vote.VoteTargetNickname = PlayersState[0].Details.Nickname;
            PlayersState[2].Vote.TColor = PlayersState[0].Details.NColor;
        }
    }
}