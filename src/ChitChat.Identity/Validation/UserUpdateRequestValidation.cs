namespace ChitChat.Identity.Validation;

public class UserUpdateRequestValidation : AbstractValidator<UserUpdateRequest>
{
    public UserUpdateRequestValidation()
    {
        RuleFor(user => user.Name).NotEmpty().NotNull().MinimumLength(3)
            .WithMessage("user name length must be minimum 3");
        RuleFor(user => user.Email).NotEmpty().EmailAddress().WithMessage("please enter a valid email address");
        
        RuleFor(user => user.DateOfBirth).Must(Validate).WithMessage("age must be above 18");
    }

    private bool Validate(DateTime time)
    {
        return time.AddYears(18) < DateTime.Now;
    }
}
