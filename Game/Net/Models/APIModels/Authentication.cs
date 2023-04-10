namespace Net.Models.APIModels;

public record class AccountDomain(string Username,
    string JwtToken,
    string RefreshToken);

public record class LoginRequest(string Username, string Password);

public record class AuthenticateResponse(long Id,
    string Username,
    string JwtToken);

public record class RegisterRequest(string Username, string Password);