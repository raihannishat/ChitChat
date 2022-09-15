namespace ChitChat.Api.Controllers;

[ApiController, Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly IUserService _userService;
    private readonly IAuthService _authService;
    private readonly ILogger<AuthController> _logger;

    public AuthController(IUserService userService, ILogger<AuthController> logger,
        IAuthService authService)
    {
        _userService = userService;
        _authService = authService;
        _logger = logger;
    }

    [HttpPost, Route("signup")]
    public async Task<IActionResult> SignUp(UserSignUpRequest user)
    {
        var userNameExist = await _userService.GetUserByNameAsync(user.Name);

        if (userNameExist != null)
        {
            return BadRequest("User name is already exist");
        }

        var emailExist = await _userService.GetUserByEmailAsync(user.Email);

        if (emailExist != null)
        {
            return BadRequest("Email address is already used before");
        }

        await _authService.SignUpAsync(user);

        return Ok();
    }


    [HttpPost, Route("signin")]
    public async Task<IActionResult> SignIn([FromBody] UserSignInRequest login)
    {
        var authResponse = await _authService.SignInAsync(login);

        if (!authResponse.Success)
        {
            return Unauthorized(new AuthFailedResponse
            {
                Errors = authResponse.Errors
            });
        }

        return Ok(new AuthSuccessResponse
        {
            Token = authResponse.Token,
            RefreshToken = authResponse.RefreshToken
        });
    }

    [HttpPost, Route("refreshToken")]
    public async Task<IActionResult> Refresh([FromBody] RefreshTokenRequest request)
    {
        var authResponse = await _authService.GetRefreshTokenAsync(request.Token, request.RefreshToken);

        if (!authResponse.Success)
        {
            return BadRequest(new AuthFailedResponse
            {
                Errors = authResponse.Errors
            });
        }

        return Ok(new AuthSuccessResponse
        {
            Token = authResponse.Token,
            RefreshToken = authResponse.RefreshToken
        });
    }
}
