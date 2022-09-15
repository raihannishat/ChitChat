namespace ChitChat.Identity.Mapping;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<User, UserSignUpRequest>().ReverseMap();
        CreateMap<User, UserUpdateRequest>().ReverseMap();
        CreateMap<User, UserViewModel>().ReverseMap();
    }
}
