using System.Windows.Media;

namespace WPFApplication.Model
{
    public class VisualPlayerData
    {
        public string Nickname { get; private set; }
        public Color NColor { get; private set; }

        public VisualPlayerData(string nickname, Color nColor)
        {
            Nickname = nickname;
            NColor = nColor;
        }
    }
}