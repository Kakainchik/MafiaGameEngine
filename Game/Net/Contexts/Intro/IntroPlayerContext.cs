using GameLogic.Model;
using Net.Models;

namespace Net.Contexts.Intro
{
    [Serializable]
    public class IntroPlayerContext : SessionContext
    {
        public RoleSignature Role { get; }
        public string Nickname { get; }
        public RGB NColor { get; }

        public IntroPlayerContext(RoleSignature role, string nickname, RGB color)
        {
            Role = role;
            Nickname = nickname;
            NColor = color;
        }
    }
}