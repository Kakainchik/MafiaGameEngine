using GameLogic.Model;
using Net.Models;
using System.Text.Json.Serialization;

namespace Net.Contexts.Morning
{
    [Serializable]
    public class VictimContext : SessionContext
    {
        public string Nickname { get; }
        public RGB NColor { get; }
        public RoleSignature Role { get; }
        public DeathReason Reason { get; }
        public string LastWill { get; }

        [JsonConstructor]
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