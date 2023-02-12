using GameRemoteServer.Entities;
using GameRemoteServer.Helpers;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace GameRemoteServer.Services
{
    public class JwtService : IJwtService
    {
        private readonly JwtSettings settings;
        private readonly ServerContext db;

        public JwtService(IOptions<JwtSettings> settings, ServerContext dbContext)
        {
            this.settings = settings.Value;
            db = dbContext;
        }

        /// <summary>
        /// Gets random token sized 40 bytes.
        /// </summary>
        /// <returns>Token string.</returns>
        public string GenerateJwtToken(User user)
        {
            //Generate token that is valid for 2 day
            byte[] key = Encoding.ASCII.GetBytes(settings.Secret);
            var tokenHandler = new JwtSecurityTokenHandler();
            var tokenDescriptor = new SecurityTokenDescriptor()
            {
                Subject = new ClaimsIdentity(new[]
                {
                    new Claim("Id", user.Id.ToString())
                }),
                Expires = DateTime.UtcNow.AddDays(2),
                Audience = settings.Audience,
                Issuer = settings.Issuer,
                SigningCredentials = new SigningCredentials(
                    new SymmetricSecurityKey(key),
                    SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }

        public RefreshToken GenerateRefreshToken()
        {
            string GetUniqueToken()
            {
                var token = Convert.ToBase64String(RandomNumberGenerator.GetBytes(48));
                var isUnique = !db.Users.Any(u => u.RefreshTokens.Any(t => t.Token == token));

                if(isUnique) return token;
                else return GetUniqueToken();
            }

            var refreshToken = new RefreshToken
            {
                Token = GetUniqueToken(),
                Created = DateTime.UtcNow,
                Experies = DateTime.UtcNow.AddMonths(2)
            };

            return refreshToken;
        }

        public int? ValidateJwtToken(string token)
        {
            byte[] key = Encoding.ASCII.GetBytes(token);
            var tokenHandler = new JwtSecurityTokenHandler();

            try
            {
                tokenHandler.ValidateToken(token, new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = true,
                    ValidIssuer = settings.Issuer,
                    ValidateAudience = true,
                    ValidAudience = settings.Audience,
                    ValidateLifetime = true,
                    ClockSkew = TimeSpan.Zero
                }, out SecurityToken validatedToken);

                var jwtToken = (JwtSecurityToken)validatedToken;
                var userId = int.Parse(jwtToken.Claims.First(x => x.Type == "Id").Value);

                return userId;
            }
            catch
            {
                return null;
            }
        }
    }
}