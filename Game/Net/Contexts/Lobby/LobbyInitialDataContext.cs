using GameLogic.Model;
using Net.Models;

namespace Net.Contexts.Lobby
{
    [Serializable]
    public class LobbyInitialDataContext : SessionContext
    {
        public int MaxPlayers { get; }
        public IDictionary<Guid, LobbyPlayer> Players { get; }
        public IDictionary<RoleSignature, int> Roles { get; }

        public LobbyInitialDataContext(int maxQuantity,
            IDictionary<Guid, LobbyPlayer> players,
            IDictionary<RoleSignature, int> roles)
        {
            MaxPlayers = maxQuantity;
            Players = players;
            Roles = roles;
        }
    }
}