namespace ChitChat.Identity.Services;

public interface IAuthService
{
    Task SignUpAsync(UserSignUpDTO user);
    Task<AuthenticationResult> SignInAsync(UserSignInDTO user);
    Task InsertRefreshTokenAsync(RefreshToken token);
    Task<AuthenticationResult> GenerateTokenAsync(User user);
    Task<AuthenticationResult> GetRefreshTokenAsync(string token, string refreshToken);
}
