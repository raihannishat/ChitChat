namespace ChitChat.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
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
    public async Task<List<User>> GetAll() =>
        await _userService.GetAllUsersAsync();


    [HttpGet("GetById/{id:length(24)}")]
    public async Task<ActionResult<User>> GetById([FromRoute] string id)
    {
        var user = await _userService.GetUserByIdAsync(id);
        if (user is null)
        {
            return NotFound();
        }
        return user;
    }

    [HttpGet("GetByName/{name}")]
    public async Task<bool> GetByName([FromRoute] string name)
    {
        var user = await _userService.GetUserByNameAsync(name);
        if (user is null)
        {
            return false;
        }
        return true;
    }

    [HttpGet("GetByEmail/{email}")]
    public async Task<bool> GetByEmail([FromRoute] string email)
    {
        var user = await _userService.GetUserByEmailAsync(email);
        if (user is null)
        {
            return false;
        }
        return true;
    }

    [HttpPut("{id:length(24)}")]
    public async Task<ActionResult<User>> Update(string id, User updateUser)
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
    public async Task<ActionResult<User>> Delete(string id)
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
