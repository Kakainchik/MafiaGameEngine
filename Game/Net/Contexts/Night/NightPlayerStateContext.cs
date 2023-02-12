using Net.Models;

namespace Net.Contexts.Night
{
    [Serializable]
    public class NightPlayerStateContext : SessionContext
    {
        public Guid Id { get; }
        public bool IsAlive { get; }
        public string Nickname { get; }
        public RGB NColor { get; }
        public bool IsOwn { get; }

        public NightPlayerStateContext(Guid id,
            string nickname,
            RGB color,
            bool isAlive,
            bool isOwn = false)
        {
            Id = id;
            Nickname = nickname;
            IsAlive = isAlive;
            NColor = color;
            IsOwn = isOwn;
        }
    }
}