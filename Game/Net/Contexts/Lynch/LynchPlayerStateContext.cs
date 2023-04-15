using Net.Models;
using System.Text.Json.Serialization;

namespace Net.Contexts.Lynch
{
    [Serializable]
    public class LynchPlayerStateContext : SessionContext
    {
        public string Nickname { get; }
        public RGB NColor { get; }
        public RoleSignature Role { get; }
        public bool IsOwn { get; }

        [JsonConstructor]
        public LynchPlayerStateContext(
            string nickname,
            RGB nColor,
            RoleSignature role,
            bool isOwn = false)
        {
            Nickname = nickname;
            NColor = nColor;
            Role = role;
            IsOwn = isOwn;
        }
    }
}