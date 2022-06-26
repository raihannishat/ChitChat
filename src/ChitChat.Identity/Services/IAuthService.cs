using ChitChat.Identity.Response;

namespace ChitChat.Identity.Services;

public interface IAuthService
{
    Task SignUpAsync(UserSignUp user);
    Task<AuthenticationResult> SignInAsync(UserSignIn user);

    Task InsertRefreshTokenAsync(RefreshToken token);
    Task<AuthenticationResult> GenerateTokenAsync(User user);

    Task<AuthenticationResult> RefreshTokenAsync(string token, string refreshToken);

}
