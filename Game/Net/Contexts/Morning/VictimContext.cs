using GameLogic.Model;
using Net.Models;

namespace Net.Contexts.Morning
{
    [Serializable]
    public class VictimContext : Context
    {
        public string Nickname { get; }
        public RGB NColor { get; }
        public RoleSignature Role { get; }
        public DeathReason Reason { get; }
        public string LastWill { get; }

        public VictimContext(string nickname,
            RGB nColor,
            RoleSignature role,
            DeathReason reason,
            string lastWill)
        {
            Nickname = nickname;
            NColor = nColor;
            Role = role;
            Reason = reason;
            LastWill = lastWill;
        }
    }
}