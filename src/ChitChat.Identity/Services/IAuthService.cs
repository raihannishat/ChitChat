namespace ChitChat.Identity.Services;

public interface IAuthService
{
    Task SignUpAsync(UserSignUpRequest user);
    Task<AuthenticationResult> SignInAsync(UserSignInRequest user);
    Task InsertRefreshTokenAsync(RefreshToken token);
    Task<AuthenticationResult> GenerateTokenAsync(UserViewModel user);
    Task<AuthenticationResult> GetRefreshTokenAsync(string token, string refreshToken);
}
