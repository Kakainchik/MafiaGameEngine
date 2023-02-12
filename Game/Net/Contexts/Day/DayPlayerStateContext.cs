using Net.Models;

namespace Net.Contexts.Day
{
    [Serializable]
    public class DayPlayerStateContext : SessionContext
    {
        public Guid Id { get; }
        public string Nickname { get; }
        public RGB NColor { get; }
        public bool IsAlive { get; }

        public DayPlayerStateContext(Guid id,
            string nickname,
            RGB color,
            bool isAlive)
        {
            Id = id;
            Nickname = nickname;
            IsAlive = isAlive;
            NColor = color;
        }
    }
}