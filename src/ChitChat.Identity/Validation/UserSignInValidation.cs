namespace ChitChat.Identity.Validation;

public class UserSingInValidation : AbstractValidator<UserSignInDTO>
{
    public UserSingInValidation()
    {
        RuleFor(user => user.Name).NotEmpty()
            .WithMessage("User name can not be empty");

        RuleFor(user => user.Password).NotEmpty()
            .WithMessage("Password can not be null");
    }
}
