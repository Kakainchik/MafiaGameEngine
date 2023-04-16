using WPFApplication.Core;
using System.Windows.Media;

namespace WPFApplication.Model
{
    public class DayPlayerState
    {
        public PlayerInfo Details { get; set; }
        public VoteInfo Vote { get; }

        public DayPlayerState(ulong id, string nickname, bool isAlive, Color color)
        {
            Details = new PlayerInfo()
            {
                Id = id,
                Nickname = nickname,
                IsAlive = isAlive,
                NColor = color
            };
            Vote = new VoteInfo();
        }
    }

    public class VoteInfo : ObservableObject
    {
        private int ownVotes;
        private string? voteTargetNickname;
        private Color tColor;
        private bool isVotesVisible;

        public int OwnVotes
        {
            get => ownVotes; 
            set
            {
                ownVotes = value;
                OnPropertyChanged(nameof(OwnVotes));

                IsVotesVisible = value > 0;
            }
        }

        public string? VoteTargetNickname
        {
            get => voteTargetNickname; 
            set
            {
                voteTargetNickname = value;
                OnPropertyChanged(nameof(VoteTargetNickname));
            }
        }

        public Color TColor
        {
            get => tColor;
            set
            {
                tColor = value;
                OnPropertyChanged(nameof(TColor));
            }
        }

        public bool IsVotesVisible
        {
            get => isVotesVisible;
            set
            {
                isVotesVisible = value;
                OnPropertyChanged(nameof(IsVotesVisible));
            }
        }
    }
}