using Net.Models;
using System.Text.Json.Serialization;

namespace Net.Contexts.Intro
{
    [Serializable]
    public class IntroPlayerContext : SessionContext
    {
        public RoleSignature Role { get; }
        public string Nickname { get; }
        public RGB NColor { get; }

        [JsonConstructor]
        public IntroPlayerContext(RoleSignature role, string nickname, RGB nColor)
        {
            Role = role;
            Nickname = nickname;
            NColor = nColor;
        }
    }
}