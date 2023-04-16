using Net.Models;
using System.Text.Json.Serialization;

namespace Net.Contexts.Night
{
    [Serializable]
    public class NightPlayerStateContext : SessionContext
    {
        public ulong Id { get; }
        public bool IsAlive { get; }
        public string Nickname { get; }
        public RGB NColor { get; }
        public bool IsOwn { get; }

        [JsonConstructor]
        public NightPlayerStateContext(ulong id,
            string nickname,
            RGB nColor,
            bool isAlive,
            bool isOwn = false)
        {
            Id = id;
            Nickname = nickname;
            IsAlive = isAlive;
            NColor = nColor;
            IsOwn = isOwn;
        }
    }
}