using Net.Models;
using System.Text.Json.Serialization;

namespace Net.Contexts.Lobby
{
    [Serializable]
    public class LobbyRoleContext : SessionContext
    {
        public IDictionary<RoleSignature, int> Roles { get; }

        [JsonConstructor]
        public LobbyRoleContext(IDictionary<RoleSignature, int> roles)
        {
            Roles = roles;
        }
    }
}