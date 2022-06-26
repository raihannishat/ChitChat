using AutoMapper;

namespace ChitChat.Identity.Dependencies;
internal class IdenityProfile : Profile
{
    public IdenityProfile()
    {
        CreateMap<User, UserSignUp>().ReverseMap();
    }
}
