namespace ChitChat.Identity.BusinessObjects;

public class UserSignUp
{
    public string Name { get; set; } = null!;
    public DateTime DateOfBirth { get; set; }
    public string Password { get; set; } = null!;
    public string Email { get; set; } = null!;
}
