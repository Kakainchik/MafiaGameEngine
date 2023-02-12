using GameRemoteServer.Entities;
using GameRemoteServer.Models;

namespace GameRemoteServer.Services
{
    public interface IUserService
    {
        Task CreateUser(ReqSignUpDTO request);
        Task<User> GetUserById(long id);
        IEnumerable<UserDTO> GetAllUsers();
    }
}