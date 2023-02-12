using Net.Models;

namespace Net.Contexts.Game
{
    [Serializable]
    public class CommonPlayerStateContext : SessionContext
    {
        public RoleSignature Role { get; }
        public bool IsAlive { get; }

        public CommonPlayerStateContext(RoleSignature role, bool isAlive)
        {
            Role = role;
            IsAlive = isAlive;
        }
    }
}