using GameRemoteServer.Entities;
using GameRemoteServer.Helpers;
using GameRemoteServer.Models;
using Microsoft.Extensions.Options;

namespace GameRemoteServer.Services
{
    public class TokenService : ITokenService
    {
        private const string CREDENTIALS_ERROR = "Username or password is incorrect.";
        private const string INVALID_TOKEN = "Invalid token.";

        private readonly JwtSettings jwtSettings;
        private readonly IJwtService jwtService;
        private readonly ServerContext db;

        public TokenService(IOptions<JwtSettings> jwtSettings,
            IJwtService jwtService,
            ServerContext dbContext)
        {
            this.jwtSettings = jwtSettings.Value;
            this.jwtService = jwtService;
            db = dbContext;
        }

        public ResAuthenticateDTO Authenticate(ReqAuthenticateDTO data)
        {
            var user = db.Users.SingleOrDefault(u => u.Username == data.Username);

            if(user == null || !PasswordHasher.ValidatePassword(data.Password, user!.Hash))
                throw new BadHttpRequestException(CREDENTIALS_ERROR, StatusCodes.Status400BadRequest);
            var refreshToken = jwtService.GenerateRefreshToken();
            user.RefreshTokens.Add(refreshToken);

            RemoveOldRefreshTokens(user);

            db.Update<User>(user);
            db.SaveChanges();

            return new ResAuthenticateDTO(user.Id,
                user.Username,
                jwtService.GenerateJwtToken(user),
                refreshToken.Token);
        }

        public ResAuthenticateDTO RefreshToken(string token)
        {
            var user = GetUserByRefreshToken(token);

            var refreshToken = user.RefreshTokens.Single(t => t.Token == token);

            if(refreshToken.IsRevoked)
            {
                RevokeDescendantRefreshTokens(refreshToken,
                    user,
                    "Attempted reuse of revoked ancestor token");
                db.Update<User>(user);
                db.SaveChanges();
            }

            if(refreshToken.IsExpired)
                throw new BadHttpRequestException(INVALID_TOKEN, StatusCodes.Status400BadRequest);

            //Replace old token with a new one
            var newRefreshToken = RotateRefreshToken(refreshToken);
            user.RefreshTokens.Add(newRefreshToken);

            //Remove old refresh tokens
            RemoveOldRefreshTokens(user);

            db.Update<User>(user);
            db.SaveChanges();

            return new ResAuthenticateDTO(user.Id,
                user.Username,
                jwtService.GenerateJwtToken(user),
                refreshToken.Token);
        }

        public void RevokeToken(string token)
        {
            var user = GetUserByRefreshToken(token);
            var refreshToken = user.RefreshTokens.Single(x => x.Token == token);

            if(!refreshToken.IsActive)
                throw new BadHttpRequestException(INVALID_TOKEN, StatusCodes.Status400BadRequest);

            //Revoke token and save
            RevokeRefreshToken(refreshToken, "Revoked without replacement");
            db.Update<User>(user);
            db.SaveChanges();
        }

        private User GetUserByRefreshToken(string token)
        {
            var user = db.Users.SingleOrDefault(u => u.RefreshTokens.Any(t => t.Token == token));
            if(user == null)
                throw new BadHttpRequestException(INVALID_TOKEN, StatusCodes.Status400BadRequest);
            return user;
        }

        private RefreshToken RotateRefreshToken(RefreshToken token)
        {
            var newRefreshToken = jwtService.GenerateRefreshToken();
            RevokeRefreshToken(newRefreshToken,
                "Replaced by new token",
                newRefreshToken.Token);

            return newRefreshToken;
        }

        private void RevokeRefreshToken(RefreshToken token,
            string? reason = null,
            string? replacedByToken = null)
        {
            token.Revoked = DateTime.UtcNow;
            token.ReasonRevoked = reason;
            token.ReplacedByToken = replacedByToken;
        }

        private void RemoveOldRefreshTokens(User user)
        {
            user.RefreshTokens.RemoveAll(t =>
                t.IsExpired &&
                t.Created.AddDays(jwtSettings.RefreshTokenTTL) <= DateTime.UtcNow);
        }

        private void RevokeDescendantRefreshTokens(RefreshToken refreshToken, User user, string reason)
        {
            //Recursively traverse the refresh token chain and ensure all descendants are revoked
            if(!string.IsNullOrEmpty(refreshToken.ReplacedByToken))
            {
                var childToken = user.RefreshTokens.SingleOrDefault(
                    x => x.Token == refreshToken.ReplacedByToken);

                if(childToken == null) return;

                if(childToken.IsActive) RevokeRefreshToken(childToken, reason);
                else RevokeDescendantRefreshTokens(childToken, user, reason);
            }
        }
    }
}