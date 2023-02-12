using WPFApplication.Model;
using System.Windows.Media;

namespace WPFApplication.Controls.Day.Design
{
    internal class DayPlayerDesign : DayPlayerControl
    {
        public DayPlayerDesign()
        {
            base.Player = new DayPlayerState(
                new System.Guid(1,1,1,1,1,1,1,1,1,1,1),
                "Kakainchik",
                false,
                Colors.Yellow);
                
            base.Player.Vote.OwnVotes = 2;
            base.Player.Vote.VoteTargetNickname = "DarkPredator";
            base.Player.Vote.TColor = Colors.SeaShell;
        }
    }
}