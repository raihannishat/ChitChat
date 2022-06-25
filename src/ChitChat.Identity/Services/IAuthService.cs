namespace ChitChat.Identity.Services;

public interface IAuthService
{
    Task SignUp(UserSignUp user);
    Task SignIn(UserSignIn user);

}
