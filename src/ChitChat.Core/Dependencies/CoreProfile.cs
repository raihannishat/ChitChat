using AutoMapper;
using ChitChat.Infrastructure.DTOs;
using ChitChat.Infrastructure.Documents;

namespace ChitChat.Infrastructure.Dependencies;
public class CoreProfile : Profile
{
    public CoreProfile()
    {
        CreateMap<Message, MessageDTO>().ReverseMap();
    }
}
