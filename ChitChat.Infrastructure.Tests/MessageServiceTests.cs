using AutoMapper;
using ChitChat.Infrastructure.Documents;
using ChitChat.Infrastructure.DTOs;
using ChitChat.Infrastructure.Repositories;
using ChitChat.Infrastructure.Services;
using FluentAssertions;
using Microsoft.Extensions.Logging;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChitChat.Infrastructure.Tests;
public class MessageServiceTests
{
    private readonly MessageService _sut;
    private readonly Mock<IMessageRepository> _messageRepoMock = new Mock<IMessageRepository>();
    private readonly Mock<ILogger<MessageService>> _loggerMock = new Mock<ILogger<MessageService>>();
    private readonly Mock<IMapper> _mapperMock = new Mock<IMapper>();

    public MessageServiceTests()
    {
        _sut = new MessageService(_messageRepoMock.Object, _mapperMock.Object, _loggerMock.Object);
    }

    [Fact]
    public void AddMessage_ShouldThrowException()
    {
        var message = new Message
        {
            Id = "fd",
            Content = "Hi"
        };

        var messageDto = new MessageDTO
        {
            Content = message.Content
        };

        //Arrange
        _messageRepoMock.Setup(x => x.InsertOneAsync(message))
            .Throws(new InvalidOperationException());

        //Act
        var result  = () =>  _sut.AddMessage(messageDto);

        //Assert
        result.Should().ThrowAsync<InvalidOperationException>();
    }
}
