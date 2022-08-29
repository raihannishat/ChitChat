namespace ChitChat.Infrastructure.Mapping;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<Message, MessageDTO>().ReverseMap();
    }
}
