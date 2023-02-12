using GameRemoteServer.Entities;
using GameRemoteServer.Helpers;
using GameRemoteServer.Models;
using Microsoft.EntityFrameworkCore;

namespace GameRemoteServer.Services
{
    public class UserService : IUserService
    {
        private const string INVALID_ID = "No user with such id.";
        private const string USERNAME_EXISTS = "A such username already exists.";
        private const string PASSWORD_WEAK = "Password does not match the security conditions.";

        private readonly ServerContext db;

        public UserService(ServerContext dbContext)
        {
            db = dbContext;
        }

        public async Task CreateUser(ReqSignUpDTO request)
        {
            bool any = await db.Users.AnyAsync(u => u.Username == request.Username);
            if(any) throw new BadHttpRequestException(USERNAME_EXISTS, StatusCodes.Status400BadRequest);

            var user = new User
            {
                Username = request.Username,
                Hash = PasswordHasher.CreateHash(request.Password)
            };

            await db.Users.AddAsync(user);
            await db.SaveChangesAsync();
        }

        public async Task<User> GetUserById(long id)
        {
            return await db.Users.SingleOrDefaultAsync(u => u.Id == id) ??
                throw new BadHttpRequestException(INVALID_ID, StatusCodes.Status404NotFound);
        }

        public IEnumerable<UserDTO> GetAllUsers()
        {
            return db.Users.Select<User, UserDTO>(u => new(u.Id, u.Username));
        }
    }
}