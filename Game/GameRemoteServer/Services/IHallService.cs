using GameRemoteServer.Entities;
using GameRemoteServer.Models;

namespace GameRemoteServer.Services
{
    public interface IHallService
    {
        IEnumerable<LobbyDTO> GetLobbies(int step);
        LobbyDTO? GetLobby(long id);
        ResCreateLobbyDTO CreateLobby(ReqCreateLobbyDTO req, User host);
        bool JoinLobby(long lobbyId, User user, out bool isHost);
        void UpdateMaxCapacity(long lobbyId, int capacity);
    }
}