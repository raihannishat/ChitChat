using ChitChat.Identity.Request;
using ChitChat.Identity.ViewModels;

namespace ChitChat.Identity.Tests;

public class UserServiceTests
{
    private readonly UserService _sut;
    public readonly Mock<IUserRepository> _userRepoMock = new();
    private readonly Mock<IMapper> _mapperMock = new();

    public UserServiceTests()
    {
        _sut = new UserService(_userRepoMock.Object, _mapperMock.Object);
    }

    [Fact]
    public void GetUserByIdAsync_ShouldReturnUser_WhenUserIdExist()
    {
        //Arrange
        var userId = "12dddf_314fdd_adfs33";

        var userDto = new User
        {
            Id = userId,
            Name = "Topu vai",
            DateOfBirth = DateTime.UtcNow,
            Email = "abc@gmail.com",
            Password = "Abc123#"
        };

        var userviewModel = new UserViewModel
        {
            Id = userDto.Id,
            Name = userDto.Name,
            DateOfBirth = userDto.DateOfBirth,
            Email = userDto.Email,
            Password = userDto.Password
        };

        _userRepoMock.Setup(x =>
            x.FindOneAsync(user => user.Id == userId)).ReturnsAsync(userDto);
        _mapperMock.Setup(x => x.Map<UserViewModel>(userDto)).Returns(userviewModel);
        //Act
        var user = _sut.GetUserByIdAsync(userId).Result;

        //Assert
        user.Should().Be(userviewModel);
    }

    [Fact]
    public void GetUserByIdAsync_ShouldReturnNULL_WhenUserIdDoesNotExist()
    {
        //Arrange
        var userId = "";

        _userRepoMock.Setup(x =>
            x.FindOneAsync(user => user.Id == userId)).ReturnsAsync(() => null!);

        //Act
        var user = _sut.GetUserByIdAsync(userId).Result;

        //Assert
        user.Should().BeNull();
    }

    [Fact]
    public void GetUserByNameAsyncs_ShouldReturnUser_WhenUserNameExist()
    {
        //Arrange
        var userName = "asif";

        var userDto = new User
        {
            Name = userName,
            DateOfBirth = DateTime.UtcNow,
            Email = "abc@gmail.com",
            Password = "Abc123#"
        };

        var userviewModel = new UserViewModel
        {
            Id = userDto.Id,
            Name = userDto.Name,
            DateOfBirth = userDto.DateOfBirth,
            Email = userDto.Email,
            Password = userDto.Password
        };

        _userRepoMock.Setup(x =>
            x.FindOneAsync(user => user.Name == userName)).ReturnsAsync(userDto);

        _mapperMock.Setup(x => x.Map<UserViewModel>(userDto)).Returns(userviewModel);
        //Act
        var user = _sut.GetUserByNameAsync(userName).Result;

        //Assert
        user.Should().Be(userviewModel);
    }

    [Fact]
    public void GetUserByNameAsync_ShouldReturnNULL_WhenUserNameDoesNotExist()
    {
        //Arrange
        var userName = "";

        _userRepoMock.Setup(x =>
            x.FindOneAsync(user => user.Name == userName)).ReturnsAsync(() => null!);

        //Act
        var user = _sut.GetUserByNameAsync(userName).Result;

        //Assert
        user.Should().BeNull();
    }

    [Fact]
    public void GetUserByEmailAsyncs_ShouldReturnUser_WhenUserEmailExist()
    {
        //Arrange
        var email = "abc@gmail.com";

        var userDto = new User
        {
            Name = "asif",
            DateOfBirth = DateTime.UtcNow,
            Email = email,
            Password = "Abc123#"
        };

        var userviewModel = new UserViewModel
        {
            Id = userDto.Id,
            Name = userDto.Name,
            DateOfBirth = userDto.DateOfBirth,
            Email = userDto.Email,
            Password = userDto.Password
        };

        _userRepoMock.Setup(x =>
            x.FindOneAsync(user => user.Email == email)).ReturnsAsync(userDto);

        _mapperMock.Setup(x => x.Map<UserViewModel>(userDto)).Returns(userviewModel);

        //Act
        var user = _sut.GetUserByEmailAsync(email).Result;

        //Assert
        user.Should().Be(userviewModel);
    }

    [Fact]
    public void GetUserByEmailAsync_ShouldReturnNULL_WhenUserEmailDoesNotExist()
    {
        //Arrange
        var email = "";

        _userRepoMock.Setup(x =>
            x.FindOneAsync(user => user.Email == email)).ReturnsAsync(() => null!);

        //Act
        var user = _sut.GetUserByEmailAsync(email).Result;

        //Assert
        user.Should().BeNull();
    }


    [Fact]
    public void GetUserAsync_ShouldReturnUser_WhenUserExist()
    {
        //Arrange 
        var userSignIn = new UserSignInRequest
        {
            Name = "nishat",
            Password = "abc123#"
        };

        User user = new User
        {
            Name = userSignIn.Name,
            Password = userSignIn.Password
        };

        var userviewModel = new UserViewModel
        {
            Name = user.Name,
            Password = user.Password
        };
        _userRepoMock.Setup(x => x.FindOneAsync(
            user => user.Name == userSignIn.Name && user.Password == userSignIn.Password))
            .ReturnsAsync(user);

        _mapperMock.Setup(x => x.Map<UserViewModel>(user)).Returns(userviewModel);
        //Act
        var userResult = _sut.GetUserAsync(userSignIn).Result;

        //Assert
        userResult.Should().Be(userviewModel);
    }

    [Fact]
    public void GetUserAsync_ShouldReturnNULL_WhenUserDoesNotExist()
    {
        //Arrange
        var userSignInDto = new UserSignInRequest
        {
            Name = "someone",
            Password = "something"
        };

        _userRepoMock.Setup(x =>
            x.FindOneAsync(x => x.Name == userSignInDto.Name && x.Password == userSignInDto.Password))
            .ReturnsAsync(() => null!);

        //Act
        var user = _sut.GetUserAsync(userSignInDto).Result;

        //Assert
        user.Should().BeNull();
    }
}
