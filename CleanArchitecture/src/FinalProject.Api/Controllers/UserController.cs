using Application.UserManagement.Commands.CreateUser;
using Application.UserManagement.Queries.GetUsers;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace FinalProject.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private IMediator _mediator;

        public UserController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("Create")]
        public async Task<IActionResult> Create([FromBody] CreateUserCommand command)
        {
            return Ok(await _mediator.Send(command));
        }

        [HttpGet("Get")]
        public async Task<IActionResult> Get([FromBody] GetUsersQuery command)
        {
            return Ok(await _mediator.Send(command));
        }
    }
}
