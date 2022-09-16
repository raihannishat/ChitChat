using ChitChat.Identity.ViewModels;

namespace ChitChat.Api.Tests;

public class UserControllerTests
{
    private readonly UserController _sut;
    private readonly Mock<IUserService> _userServiceMock = new();
    private readonly Mock<ILogger<UserController>> _loggerMock = new();

    public UserControllerTests()
    {
        _sut = new UserController(_userServiceMock.Object, _loggerMock.Object);
    }

    [Fact]
    public void GetAll_ShouldReturnOk_WhenUsersListFound()
    {
        //Arrange
        var usersList = new List<UserViewModel>()
        {
            new UserViewModel { Id = "1", Name = "asif", Email = "abc@gmail.com", Password = "as", DateOfBirth = DateTime.Now},
            new UserViewModel { Id = "2", Name = "nishat", Email = "abc@gmail.com", Password = "as", DateOfBirth = DateTime.Now},
            new UserViewModel { Id = "3", Name = "pawpaw", Email = "abc@gmail.com", Password = "as", DateOfBirth = DateTime.Now}
        };

        _userServiceMock.Setup(x => x.GetAllUsersAsync()).ReturnsAsync(usersList);

        //Act
        var result = _sut.GetAll().Result;

        //Assert
        result.GetType().Should().Be(typeof(OkObjectResult));
        (result as OkObjectResult)!.StatusCode.Should().Be(200);
    }

    [Fact]
    public void GetById_ShouldReturnOk_WhenUserExist()
    {
        //Arrange 
        string userId = "12adsd_dfd4_sf3";

        var user = new UserViewModel
        {
            Name = "asif",
            Email = "asif@gmail.com",
            Password = "something123#",
            DateOfBirth = DateTime.Now
        };

        _userServiceMock.Setup(x => x.GetUserByIdAsync(userId)).ReturnsAsync(user);

        //Act
        var result = _sut.GetById(userId).Result;

        //Assert
        result.GetType().Should().Be(typeof(OkObjectResult));
        (result as OkObjectResult)!.StatusCode.Should().Be(200);
    }

    [Fact]
    public void GetById_ShouldReturnNotFound_WhenUserDoesNotExist()
    {
        //Arrange 
        string userId = "12adsd";
        _userServiceMock.Setup(x => x.GetUserByIdAsync(userId)).ReturnsAsync(()
            => null!);

        //Act
        var result = _sut.GetById(userId).Result;

        //Assert
        result.GetType().Should().Be(typeof(NotFoundResult));
        (result as NotFoundResult)!.StatusCode.Should().Be(404);
    }

   
    [Fact]
    public void GetByName_ShouldReturnOk_WhenUserNameExist()
    {
        //Arrange
        string userName = "nishat";

        var user = new UserViewModel
        {
            Name = "nishat",
            Email = "nishat@gmail.com",
            Password = "something123#",
            DateOfBirth = DateTime.Now
        };

        _userServiceMock.Setup(x => x.GetUserByNameAsync(userName)).ReturnsAsync(user);

        //Act
        var result = _sut.GetByName(userName).Result;

        //Assert
        result.GetType().Should().Be(typeof(OkObjectResult));
        (result as OkObjectResult)!.StatusCode.Should().Be(200);
    }

    [Fact]
    public void GetByName_ShouldReturnOk_WhenUserNameDoesNotExist()
    {
        //Arrange
        string userName = "omuk";

        _userServiceMock.Setup(x => x.GetUserByNameAsync(userName)).ReturnsAsync(()
            => null!);

        //Act
        var result = _sut.GetByName(userName).Result;

        //Assert
        result.GetType().Should().Be(typeof(OkObjectResult));
        (result as OkObjectResult)!.StatusCode.Should().Be(200);
    }

    [Fact]
    public void GetByEmail_ShouldReturnOk_WhenUserEmailExist()
    {
        //Arrange
        string email = "asif@gmail.com";

        var user = new UserViewModel
        {
            Name = "nishat",
            Email = email,
            Password = "something123#",
            DateOfBirth = DateTime.Now
        };

        _userServiceMock.Setup(x => x.GetUserByEmailAsync(email)).ReturnsAsync(user);

        //Act
        var result = _sut.GetByEmail(email).Result;

        //Assert
        result.GetType().Should().Be(typeof(OkObjectResult));
        (result as OkObjectResult)!.StatusCode.Should().Be(200);
    }

    [Fact]
    public void GetByEmail_ShouldReturnOk_WhenUserEmailDoesNotExist()
    {
        //Arrange
        string email = "omuk@gmail.com";

        _userServiceMock.Setup(x => x.GetUserByEmailAsync(email))
            .ReturnsAsync(() => null!);

        //Act
        var result = _sut.GetByEmail(email).Result;

        //Assert
        result.GetType().Should().Be(typeof(OkObjectResult));
        (result as OkObjectResult)!.StatusCode.Should().Be(200);
    }

    [Fact]
    public void Update_ShouldReturnNotFound_WhenUserDoesNotExist()
    {
        //Arrange
        string userId = "4dsd_3adfd_dd31";

        _userServiceMock.Setup(x => 
            x.UpdateUserAsync(userId, new UserUpdateRequest())).ReturnsAsync(false);

        //Act
        var result = _sut.Update(userId, new()).Result;
        var obj = result.Result as NotFoundResult;

        //Assert
        result.Result.Should().BeOfType<NotFoundResult>();
        obj!.StatusCode.Should().Be(404);
    }

    [Fact]
    public void Update_ShouldReturnNoContent_WhenUpdateSuccessfully()
    {
        //Arrange
        string userId = "fd3431";

        var updateuser = new UserUpdateRequest
        {
            Name = "asif",
            Email = "changed@gmail.com"
        };

        _userServiceMock.Setup(x => 
            x.UpdateUserAsync(userId, updateuser)).ReturnsAsync(true);

        //Act
        var result = _sut.Update(userId, updateuser).Result;
        var obj = result.Result as NoContentResult;

        //Assert
        obj!.StatusCode.Should().Be(204);
    }

    [Fact]
    public void Delete_ShouldReturnNotFoundResult_WhenUserDoesNotExist()
    {
        //Arrange
        string userId = "66rafdf23";
        _userServiceMock.Setup(x =>
            x.DeleteUserByIdAsync(userId)).ReturnsAsync(false);
        
        //Act
        var result = _sut.Delete(userId).Result;
        var obj = result.Result as NotFoundResult;

        //Assert
        result.Result.Should().BeOfType<NotFoundResult>();
        obj!.StatusCode.Should().Be(404);
    }

    [Fact]
    public void Delete_ShouldReturnNoContent_WhenDeletedSuccessfully()
    {
        //Arrange
        string userId = "fd3431";

        _userServiceMock.Setup(x => 
            x.DeleteUserByIdAsync(userId)).ReturnsAsync(true);

        //Act
        var result = _sut.Delete(userId).Result;
        var obj = result.Result as NoContentResult;

        //Assert
        obj!.StatusCode.Should().Be(204);
    }
}
