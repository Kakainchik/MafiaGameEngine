using WPFApplication.Core;
using System.Windows.Media;

namespace WPFApplication.Model
{
    public class PlayerInfo : ObservableObject
    {
        private ulong id;
        private string? nickname;
        private bool isAlive;
        private Color nColor;

        public ulong Id
        {
            get => id; 
            set
            {
                id = value;
                OnPropertyChanged(nameof(Id));
            }
        }

        public string? Nickname
        {
            get => nickname; 
            set
            {
                nickname = value;
                OnPropertyChanged(nameof(Nickname));
            }
        }

        public bool IsAlive
        {
            get => isAlive; 
            set
            {
                isAlive = value;
                OnPropertyChanged(nameof(IsAlive));
            }
        }

        public Color NColor
        {
            get => nColor;
            set
            {
                nColor = value;
                OnPropertyChanged(nameof(NColor));
            }
        }
    }
}