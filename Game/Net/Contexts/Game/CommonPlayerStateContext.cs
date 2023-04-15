using Net.Models;
using System.Text.Json.Serialization;

namespace Net.Contexts.Game
{
    [Serializable]
    public class CommonPlayerStateContext : SessionContext
    {
        public RoleSignature Role { get; }
        public bool IsAlive { get; }

        [JsonConstructor]
        public CommonPlayerStateContext(RoleSignature role, bool isAlive)
        {
            Role = role;
            IsAlive = isAlive;
        }
    }
}