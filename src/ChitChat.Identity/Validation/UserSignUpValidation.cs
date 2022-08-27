namespace ChitChat.Identity.Validation;

public class UserSingUpValidation : AbstractValidator<UserSignUpDTO>
{
    public UserSingUpValidation()
    {
        RuleFor(user => user.Name).NotEmpty().NotNull().MinimumLength(3)
            .WithMessage("user name length must be minimum 3");
        RuleFor(user => user.Email).NotEmpty().EmailAddress().WithMessage("please enter a valid email address");
        RuleFor(user => user.Password)
            .Matches(@"^(?=.*[A-Za-z])(?=.*?[0-9])(?=.*?[!@#$&*~]).{6,}$")
            .WithMessage("Password must contain 1 letter, 1 special character and 1 character").MinimumLength(6)
            .WithMessage("password length must be minimum 6 characters");
        RuleFor(user => user.DateOfBirth).Must(Validate).WithMessage("age must be above 18");
    }

    private bool Validate(DateTime time)
    {
        return time.AddYears(18) < DateTime.Now;
    }
}
