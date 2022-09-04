using Application.Shared.Constants;
using FluentValidation;

namespace Application.UserManagement.Commands.CreateUser
{
    public class CreateUserCommandValidator : AbstractValidator<CreateUserCommand>
    {
        public CreateUserCommandValidator()
        {
            RuleFor(x => x.Name).NotEmpty().NotNull().WithMessage(MessageConstant.UserMustNotBeEmpty);
        }
    }
}
