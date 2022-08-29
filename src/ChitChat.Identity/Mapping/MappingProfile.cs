namespace ChitChat.Identity.Mapping;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<User, UserSignUpDTO>().ReverseMap();
    }
}
