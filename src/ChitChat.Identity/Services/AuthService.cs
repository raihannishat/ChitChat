namespace ChitChat.Identity.Services;

public  class AuthService : IAuthService
{
    private readonly IUserRepository _userRepository;
    private readonly IAuthRepository _authRepository;
    public AuthService(IAuthRepository authRepository)
    {
        _authRepository = authRepository;
    }

    public Task SignIn(UserSignIn user)
    {
        throw new NotImplementedException();
    }

    public Task SignUp(UserSignUp user)
    {
        throw new NotImplementedException();
    }
}
