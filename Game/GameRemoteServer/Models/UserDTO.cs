using GameRemoteServer.Helpers;
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace GameRemoteServer.Models
{
    public record UserDTO(
        long Id,
        string Username);

    public record ReqSignUpDTO(
        [StringLength(50, MinimumLength = 5)] string Username,
        [RegularExpression(Constants.PASSWORD_REGEX)] string Password);

    public record ReqAuthenticateDTO(
        string Username,
        string Password);

    public record ResAuthenticateDTO
    {
        
        public long Id { get; init; }
        public string Username { get; init; }
        public string JwtToken { get; init; }

        [JsonIgnore]
        public string RefreshToken { get; init; }

        public ResAuthenticateDTO(long id, string username, string jwtToken, string refreshToken)
        {
            Id = id;
            Username = username;
            JwtToken = jwtToken;
            RefreshToken = refreshToken;
        }
    }
}