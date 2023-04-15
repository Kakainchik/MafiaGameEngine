using Net.Models;
using System.Text.Json.Serialization;

namespace Net.Contexts.Lobby
{
    [Serializable]
    public class LobbyInitialDataContext : SessionContext
    {
        public int MaxQuantityPlayers { get; }
        public IDictionary<Guid, LobbyPlayer> Players { get; }
        public IDictionary<RoleSignature, int> Roles { get; }

        [JsonConstructor]
        public LobbyInitialDataContext(int maxQuantityPlayers,
            IDictionary<Guid, LobbyPlayer> players,
            IDictionary<RoleSignature, int> roles)
        {
            MaxQuantityPlayers = maxQuantityPlayers;
            Players = players;
            Roles = roles;
        }
    }
}