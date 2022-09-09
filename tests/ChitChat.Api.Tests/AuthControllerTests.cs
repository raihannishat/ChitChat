namespace ChitChat.Api.Tests;

public class AuthControllerTests
{
    private readonly AuthController _sut;
    private readonly Mock<IUserService> _userServiceMock = new();
    private readonly Mock<IAuthService> _authServiceMock = new();
    private readonly Mock<ILogger<AuthController>> _loggerMock = new();

    public AuthControllerTests()
    {
        _sut = new AuthController(_userServiceMock.Object, _loggerMock.Object, _authServiceMock.Object);
    }

    [Fact]
    public void SignUp_ShouldReturnBadRequest_WhenUserNameAlreadyExist()
    {
        //Arrange
        var userSignUpDto = new UserSignUpDTO
        {
            Name = "asif",
            Email = "asif@gmail.com",
            Password = "something",
            DateOfBirth = DateTime.Now
        };

        var user = new User
        {
            Name = userSignUpDto.Name,
            Email = userSignUpDto.Email,
            Password = userSignUpDto.Password,
            DateOfBirth = DateTime.Now
        };

        _userServiceMock.Setup(x => x.GetUserByNameAsync(userSignUpDto.Name))
            .ReturnsAsync(user);

        //Act
        var result = _sut.SignUp(userSignUpDto).Result;
        var obj = result as BadRequestObjectResult;

        //Assert
        obj!.StatusCode.Should().Be(400);
    }

    [Fact]
    public void SignUp_ShouldReturnBadRequest_WhenUserEmailAlreadyExist()
    {
        //Arrange
        var userSignUpDto = new UserSignUpDTO
        {
            Name = "asif",
            Email = "asif@gmail.com",
            Password = "something",
            DateOfBirth = DateTime.Now
        };

        var user = new User
        {
            Name = userSignUpDto.Name,
            Email = userSignUpDto.Email,
            Password = userSignUpDto.Password,
            DateOfBirth = DateTime.Now
        };

        _userServiceMock.Setup(x => x.GetUserByNameAsync(userSignUpDto.Name))
            .ReturnsAsync(() => null!);
        _userServiceMock.Setup(x => x.GetUserByEmailAsync(userSignUpDto.Email))
            .ReturnsAsync(user);

        //Act
        var result = _sut.SignUp(userSignUpDto).Result;
        var obj = result as BadRequestObjectResult;

        //Assert
        obj!.StatusCode.Should().Be(400);
    }

    [Fact]
    public void SignUp_ShouldReturnOK_WhenUserSignUpSuccesfully()
    {
        //Arrange
        var userSignUpDto = new UserSignUpDTO
        {
            Name = "asif",
            Email = "asif@gmail.com",
            Password = "something",
            DateOfBirth = DateTime.Now
        };

        var user = new User
        {
            Name = userSignUpDto.Name,
            Email = userSignUpDto.Email,
            Password = userSignUpDto.Password,
            DateOfBirth = DateTime.Now
        };

        _userServiceMock.Setup(x => x.GetUserByNameAsync(userSignUpDto.Name))
            .ReturnsAsync(() => null!);
        _userServiceMock.Setup(x => x.GetUserByEmailAsync(userSignUpDto.Email))
            .ReturnsAsync(() => null!);

        _authServiceMock.Setup(x => x.SignUpAsync(userSignUpDto));

        //Act
        var result = _sut.SignUp(userSignUpDto).Result;
        var obj = result as OkResult;

        //Assert
        obj!.StatusCode.Should().Be(200);
    }


    [Fact]
    public void SignIn_ShouldReturnUnAuthorized_WhenUserCredentialIsInvalid()
    {
        //Arrange
        var userLogin = new UserSignInDTO
        {
            Name = "asif",
            Password = "addd"
        };

        var authResult = new AuthenticationResult
        {
            Success = false
        };

        _authServiceMock.Setup(x => x.SignInAsync(userLogin))
            .ReturnsAsync(authResult);

        //Act
        var result = _sut.SignIn(userLogin).Result;
        var obj = result as UnauthorizedObjectResult;

        //Assert
        obj!.StatusCode.Should().Be(401);
    }


    [Fact]
    public void SignIn_ShouldReturnOk_WhenUserCredentialIsvalid()
    {
        //Arrange
        var userLogin = new UserSignInDTO
        {
            Name = "asif",
            Password = "addd"
        };

        var authResult = new AuthenticationResult
        {
            Success = true
        };

        _authServiceMock.Setup(x => x.SignInAsync(userLogin))
            .ReturnsAsync(authResult);

        //Act
        var result = _sut.SignIn(userLogin).Result;
        var obj = result as OkObjectResult;

        //Assert
        obj!.StatusCode.Should().Be(200);
    }

    [Fact]
    public void Refresh_ShouldReturnBadRequest_WhenRefreshTokenIsInvalid()
    {
        //Arrange
        var refreshTokenDto = new RefreshTokenRequest
        {
            Token = "fdfsddfs445w4dfsdf",
            RefreshToken = "sdsdsd343rfdsf"
        };

        var authResponse = new AuthenticationResult
        {
            Success = false
        };

        _authServiceMock.Setup(x =>
            x.GetRefreshTokenAsync(refreshTokenDto.Token, refreshTokenDto.RefreshToken))
            .ReturnsAsync(authResponse);

        //Act
        var result = _sut.Refresh(refreshTokenDto).Result;
        var obj = result as BadRequestObjectResult;

        //Assert
        obj!.StatusCode.Should().Be(400);
    }


    [Fact]
    public void Refresh_ShouldReturnOk_WhenRefreshTokenIsValid()
    {
        //Arrange
        var refreshTokenDto = new RefreshTokenRequest
        {
            Token = "fdfsddfs445w4dfsdf",
            RefreshToken = "sdsdsd343rfdsf"
        };

        var authResponse = new AuthenticationResult
        {
            Success = true
        };

        _authServiceMock.Setup(x =>
            x.GetRefreshTokenAsync(refreshTokenDto.Token, refreshTokenDto.RefreshToken))
            .ReturnsAsync(authResponse);

        //Act
        var result = _sut.Refresh(refreshTokenDto).Result;
        var obj = result as OkObjectResult;

        //Assert
        obj!.StatusCode.Should().Be(200);
    }
}
