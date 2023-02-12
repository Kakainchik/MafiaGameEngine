using GameRemoteServer.Entities;

namespace GameRemoteServer.Services
{
    public interface IJwtService
    {
        string GenerateJwtToken(User user);
        RefreshToken GenerateRefreshToken();
        int? ValidateJwtToken(string token);
    }
}