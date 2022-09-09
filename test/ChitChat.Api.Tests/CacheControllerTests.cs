using ChitChat.Api.Controllers;
using ChitChat.Data.DTOs;
using ChitChat.Data.Services;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChitChat.Api.Tests;
public class CacheControllerTests
{
    private readonly CacheController _sut;
    private readonly Mock<ICacheService> _cacheServiceMock = new Mock<ICacheService>();

    public CacheControllerTests()
    {
        _sut = new CacheController(_cacheServiceMock.Object);
    }

    [Fact]
    public void GetAllKeys_ShouldReturnOK_WhenFoundAllKeys()
    {
        //Arrange
        List<string> keys = new List<string>() { "asif", "nishat " };
        _cacheServiceMock.Setup(x => x.GetAllKeys()).Returns(keys);

        //Act
        var result  = _sut.GetAllKeys();
        var obj = result as OkObjectResult;

        //Assert
        obj.StatusCode.Should().Be(200);
    }

    [Fact]
    public void GetAllKeys_ShouldReturnNotFound_WhenThrowException()
    {
        //Arrange
        List<string> keys = new List<string>() { "asif", "nishat " };
        _cacheServiceMock.Setup(x => x.GetAllKeys()).Throws(new Exception());

        //Act
        var result = _sut.GetAllKeys();
        var obj = result as NotFoundResult;

        //Assert
        obj.StatusCode.Should().Be(404);
    }

    [Fact]
    public void Post_ShouldRetunrBadRequest_WhenRequestKeyIsNull()
    {
        //Arrange
        var request = new CacheEntryDTO
        {
            Key = "",
            Value = ""
        };
        //Act
        var result = _sut.Post(request).Result;
        var obj = result as BadRequestObjectResult;

        //Assert
        obj.StatusCode.Should().Be(400);
    }

    [Fact]
    public void Post_ShouldRetunrOK_WhenKeySetSuccessfully()
    {
        //Arrange
        var request = new CacheEntryDTO
        {
            Key = "asif",
            Value = ""
        };

        _cacheServiceMock.Setup(x => x.SetCachValueAsync(request.Key, request.Value));
        //Act
        var result = _sut.Post(request).Result;
        var obj = result as OkResult;

        //Assert
        obj.StatusCode.Should().Be(200);
    }

    [Fact]
    public void Post_ShouldRetunrBadRequest_WhenThrownExveption()
    {
        //Arrange
        var request = new CacheEntryDTO
        {
            Key = "asif",
            Value = ""
        };

        _cacheServiceMock.Setup(x =>
            x.SetCachValueAsync(request.Key, request.Value)).Throws(new Exception());
        //Act
        var result = _sut.Post(request).Result;
        var obj = result as BadRequestResult;

        //Assert
        obj.StatusCode.Should().Be(400);
    }
}
