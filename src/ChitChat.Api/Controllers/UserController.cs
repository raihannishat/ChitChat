
namespace ChitChat.Api.Controllers;

[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme),
    ApiController, Route("api/[controller]")]
public class UserController : ControllerBase
{
    private readonly IUserService _userService;
    private readonly ILogger<UserController> _logger;

    public UserController(IUserService userService, ILogger<UserController> logger)
    {
        _userService = userService;
        _logger = logger;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var users = await _userService.GetAllUsersAsync();
        return Ok(users);
    }
    [HttpGet("GetById/{id}")]
    public async Task<IActionResult> GetById([FromRoute] string id)
    {
            var user = await _userService.GetUserByIdAsync(id);
            if (user == null)
            {
                return NotFound();
            }
            return Ok(user);
    }

    [AllowAnonymous]
    [HttpGet("GetByName/{name}")]
    public async Task<IActionResult> GetByName([FromRoute] string name)
    {
        var user = await _userService.GetUserByNameAsync(name);
        
        if (user == null)
        {
            return Ok(false);
        }
        return Ok(true);
    }

    [AllowAnonymous]
    [HttpGet("GetByEmail/{email}")]
    public async Task<IActionResult> GetByEmail([FromRoute] string email)
    {
        var user = await _userService.GetUserByEmailAsync(email);

        if (user == null)
        {
            return Ok(false);
        }
        return Ok(true);
    }

    [HttpPut("{id:length(24)}")]
    public async Task<ActionResult<UserViewModel>> Update(string id, UserUpdateRequest updateUser)
    {
        var user = await _userService.GetUserByIdAsync(id);

        if (user is null)
        {
            return NotFound();
        }

        updateUser.Id = id;
        await _userService.UpdateUserAsync(updateUser);

        return NoContent();
    }

    [HttpDelete("{id:length(24)}")]
    public async Task<ActionResult<UserViewModel>> Delete(string id)
    {
        var user = await _userService.GetUserByIdAsync(id);

        if (user is null)
        {
            return NotFound();
        }

        await _userService.DeleteUserByIdAsync(id);

        return NoContent();
    }
}
