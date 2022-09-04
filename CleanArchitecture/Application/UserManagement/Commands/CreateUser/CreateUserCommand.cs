using Domain.Entities;
using MediatR;

namespace Application.UserManagement.Commands.CreateUser
{
    public class CreateUserCommand : IRequest<User>
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = null!;
    }
}
