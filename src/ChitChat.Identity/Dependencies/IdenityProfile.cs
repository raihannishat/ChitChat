namespace ChitChat.Identity.Dependencies;

public class IdenityProfile : Profile
{
    public IdenityProfile()
    {
        CreateMap<User, UserSignUpDTO>().ReverseMap();
    }
}
