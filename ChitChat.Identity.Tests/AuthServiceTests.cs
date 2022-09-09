using AutoMapper;
using ChitChat.Identity.Configuration;
using ChitChat.Identity.Documents;
using ChitChat.Identity.DTOs;
using ChitChat.Identity.Repositories;
using ChitChat.Identity.Response;
using ChitChat.Identity.Services;
using ChitChat.Identity.Utilities;
using FluentAssertions;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChitChat.Identity.Tests;
public class AuthServiceTests
{
    private readonly AuthService _sut;
    private readonly Mock<IAuthService> _authServiceMock = new Mock<IAuthService>();
    private readonly Mock<IAuthRepository> _authRepoMock = new Mock<IAuthRepository>();
    private readonly Mock<IUserService> _userServiceMock = new Mock<IUserService>();
    private readonly Mock<IMapper> _mapperMock = new Mock<IMapper>();
    private readonly Mock<ITokenHelper> _tokenHelperMock = new Mock<ITokenHelper>();
    private readonly Mock<IJwtSettings> _jwtSettingMock = new Mock<IJwtSettings>();

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
        _userServiceMock.Setup(x => x.GetUserAsync(userDto)).ReturnsAsync(() => null);

        //Act

        var result = _sut.SignInAsync(userDto).Result;
        //Assert

        result.Success.Should().Be(false);
    }


}
