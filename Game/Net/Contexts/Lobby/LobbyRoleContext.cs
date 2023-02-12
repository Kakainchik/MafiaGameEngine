using Net.Models;

namespace Net.Contexts.Lobby
{
    [Serializable]
    public class LobbyRoleContext : SessionContext
    {
        public IDictionary<RoleSignature, int> Roles { get; }

        public LobbyRoleContext(IDictionary<RoleSignature, int> roles)
        {
            Roles = roles;
        }
    }
}