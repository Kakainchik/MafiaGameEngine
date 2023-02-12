using GameRemoteServer.Entities;
using GameRemoteServer.Models;
using Net.Models;
using System.Collections.Concurrent;

namespace GameRemoteServer.Services
{
    public class PublicHallService : IHallService
    {
        private const string NOT_FOUND_ERROR = "Lobby not found.";
        private const string MULTIPLE_LOBBIES_ERROR = "Multiple lobbies forbidden.";

        private readonly ILogger<PublicHallService> logger;

        private ConcurrentBag<LobbyDomain> lobbies;

        public PublicHallService(ILogger<PublicHallService> logger)
        {
            this.logger = logger;
            lobbies = new ConcurrentBag<LobbyDomain>();
        }

        public IEnumerable<LobbyDTO> GetLobbies(int step = 1)
        {
            if(step < 1) throw new ArgumentOutOfRangeException(nameof(step));

            const int COUNT = 20;
            Range range = (COUNT * step - COUNT)..(COUNT * step);
            return lobbies.Take(range)
                .Select<LobbyDomain, LobbyDTO>(
                    l => new LobbyDTO(l.Id, l.Title, l.Host.Username, l.Fullness, l.MaxSeats, l.IsFull));
        }

        public LobbyDTO GetLobby(long id)
        {
            var domain = lobbies.SingleOrDefault(l => l.Id == id);
            if(domain == null)
                throw new BadHttpRequestException(NOT_FOUND_ERROR, StatusCodes.Status404NotFound);

            return new LobbyDTO(domain.Id,
                domain.Title,
                domain.Host.Username,
                domain.Fullness,
                domain.MaxSeats,
                domain.IsFull);
        }

        public ResCreateLobbyDTO CreateLobby(ReqCreateLobbyDTO req, User host)
        {
            if(lobbies.Any(l => l.Host.Equals(host)))
                throw new BadHttpRequestException(MULTIPLE_LOBBIES_ERROR,
                    StatusCodes.Status405MethodNotAllowed);

            var domain = new LobbyDomain
            {
                Host = host,
                Title = req.Title,
                Description = req.Description,
                MaxSeats = req.MaxSeats,
                Players = new Dictionary<long, LobbyPlayer>(req.MaxSeats)
            };
            lobbies.Add(domain);

            return new ResCreateLobbyDTO(domain.Id);
        }

        public bool JoinLobby(long lobbyId, User user, out bool isHost)
        {
            isHost = false;

            var lobby = lobbies.SingleOrDefault(l => l.Id == lobbyId);
            if(lobby != null)
            {
                bool exists = lobby.Players.ContainsKey(user.Id);
                if(!exists)
                {
                    isHost = lobby.Host.Equals(user);
                    LobbyPlayer lb = new LobbyPlayer(user.Username, isHost);
                    lobby.Players.Add(user.Id, lb);
                }
                return exists;
            }
            else return false;
        }

        public void UpdateMaxCapacity(long lobbyId, int capacity)
        {
            var lobby = lobbies.SingleOrDefault(l => l.Id == lobbyId);
            if(lobby != null)
            {
                lobby.MaxSeats = capacity;
            }
        }
    }
}