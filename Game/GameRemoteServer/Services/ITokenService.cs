using GameRemoteServer.Models;

namespace GameRemoteServer.Services
{
    public interface ITokenService
    {
        ResAuthenticateDTO Authenticate(ReqAuthenticateDTO request);
        ResAuthenticateDTO RefreshToken(string token);
        void RevokeToken(string token);
    }
}