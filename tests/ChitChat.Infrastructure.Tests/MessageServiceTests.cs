namespace ChitChat.Infrastructure.Tests;

public class MessageServiceTests
{
    private readonly MessageService _sut;
    private readonly Mock<IMessageRepository> _messageRepoMock = new();
    private readonly Mock<ILogger<MessageService>> _loggerMock = new();
    private readonly Mock<IMapper> _mapperMock = new();

    public MessageServiceTests()
    {
        _sut = new MessageService(_messageRepoMock.Object, _mapperMock.Object, _loggerMock.Object);
    }

    [Fact]
    public void AddMessage_ShouldThrowException()
    {
        //Arrange
        var message = new Message
        {
            Id = "fd",
            Content = "Hi"
        };

        var messageDto = new MessageDTO
        {
            Content = message.Content
        };

        _messageRepoMock.Setup(x => x.InsertOneAsync(message))
            .Throws(new InvalidOperationException());

        //Act
        var result = () => _sut.AddMessage(messageDto);

        //Assert
        result.Should().ThrowAsync<InvalidOperationException>();
    }
}
