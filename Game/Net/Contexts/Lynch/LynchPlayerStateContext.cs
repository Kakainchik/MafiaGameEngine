using GameLogic.Model;
using Net.Models;

namespace Net.Contexts.Lynch
{
    [Serializable]
    public class LynchPlayerStateContext : SessionContext
    {
        public string Nickname { get; }
        public RGB NColor { get; }
        public RoleSignature Role { get; }
        public bool IsOwn { get; }

        public LynchPlayerStateContext(
            string nickname,
            RGB color,
            RoleSignature role,
            bool isOwn = false)
        {
            Nickname = nickname;
            NColor = color;
            Role = role;
            IsOwn = isOwn;
        }
    }
}