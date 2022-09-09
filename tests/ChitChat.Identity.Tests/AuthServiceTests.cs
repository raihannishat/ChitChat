namespace ChitChat.Identity.Tests;

public class AuthServiceTests
{
    private readonly AuthService _sut;
    private readonly Mock<IAuthService> _authServiceMock = new();
    private readonly Mock<IAuthRepository> _authRepoMock = new();
    private readonly Mock<IUserService> _userServiceMock = new();
    private readonly Mock<IMapper> _mapperMock = new();
    private readonly Mock<ITokenHelper> _tokenHelperMock = new();
    private readonly Mock<IJwtSettings> _jwtSettingMock = new();

    public AuthServiceTests()
    {
        _sut = new AuthService(_authRepoMock.Object, _userServiceMock.Object, _mapperMock.Object,
            _tokenHelperMock.Object, _jwtSettingMock.Object);
    }

    [Fact]
    public void SignInAsync_AuthenticationResultSuccessShouldBeFalse_WhenUserCredentialIsInvalid()
    {
        //Arrange
        var userDto = new UserSignInDTO
        {
            Name = "nishat",
            Password = "something"
        };

        _userServiceMock.Setup(x => x.GetUserAsync(userDto)).ReturnsAsync(() => null!);

        //Act
        var result = _sut.SignInAsync(userDto).Result;

        //Assert
        result.Success.Should().Be(false);
    }
}
