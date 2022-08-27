namespace ChitChat.Infrastructure.Dependencies;

public class InfrastructureProfile : Profile
{
    public InfrastructureProfile()
    {
        CreateMap<Message, MessageDTO>().ReverseMap();
    }
}
