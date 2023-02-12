namespace Net.Models
{
    [Serializable]
    public class SessionPLayer
    {
        public string Nickname { get; }
        public RGB NColor { get; }

        public SessionPLayer(string nickname, RGB nColor)
        {
            Nickname = nickname;
            NColor = nColor;
        }
    }
}