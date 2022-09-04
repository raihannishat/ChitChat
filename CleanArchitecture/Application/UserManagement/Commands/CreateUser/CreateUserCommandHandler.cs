using Domain.Entities;
using MediatR;

namespace Application.UserManagement.Commands.CreateUser
{
    public class CreateUserCommandHandler : IRequestHandler<CreateUserCommand, User>
    {
        public async Task<User> Handle(CreateUserCommand request, CancellationToken cancellationToken)
        {
            //var user = _mapper.Map<User>(request);
            //await _userRepository.Create(user);
            throw new NotImplementedException();
        }
    }
}
