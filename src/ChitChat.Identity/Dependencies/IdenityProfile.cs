namespace ChitChat.Identity.Dependencies;

public class IdenityProfile : Profile
{
    public IdenityProfile()
    {
        CreateMap<User, UserSignUp>().ReverseMap();
    }
}
