using WPFApplication.Core;
using System.Windows.Media;

namespace WPFApplication.Model
{
    public class NightPlayerState : ObservableObject
    {
        private bool isPicked;

        public PlayerInfo Details { get; set; }
        public bool IsOwn { get; }

        public bool IsPicked
        {
            get => isPicked;
            set
            {
                isPicked = value;
                OnPropertyChanged(nameof(IsPicked));
            }
        }

        public NightPlayerState(ulong id,
            string nickname,
            bool isAlive,
            Color color,
            bool isOwn)
        {
            Details = new PlayerInfo()
            {
                Id = id,
                Nickname = nickname,
                IsAlive = isAlive,
                NColor = color
            };
            IsOwn = isOwn;
        }
    }
}