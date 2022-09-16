namespace ChitChat.Identity.Services;

public class UserService : IUserService
{
    private readonly IUserRepository _userRepository;
    private readonly IMapper _mapper;

    public UserService(IUserRepository userRepository, IMapper mapper)
    {
        _userRepository = userRepository;
        _mapper = mapper;
    }

    public async Task CreateUserAysnc(User user)
    {
        await _userRepository.InsertOneAsync(user);
    }

    public async Task<bool> DeleteUserByIdAsync(string id)
    {
        var user = await GetUserByIdAsync(id);

        if (user is null)
        {
            return false;
        }
        await _userRepository.DeleteByIdAsync(id);
        
        return true;
    }

    public async Task<List<UserViewModel>> GetAllUsersAsync()
    {
        var userList = await _userRepository.GetAll();

        var userViewModelList = new List<UserViewModel>();
        foreach (var user in userList)
        {
            userViewModelList.Add(_mapper.Map<UserViewModel>(user));
        }

        return userViewModelList;
    }

    public async Task<UserViewModel> GetUserAsync(UserSignInRequest user)
    {
        var userEntity = await _userRepository.FindOneAsync(x => x.Name == user.Name && x.Password == user.Password);
        return _mapper.Map<UserViewModel>(userEntity);
    }

    public async Task<UserViewModel> GetUserByIdAsync(string id)
    {
        var userEntity = await _userRepository.FindOneAsync(user => user.Id == id);
        return _mapper.Map<UserViewModel>(userEntity);
    }

    public async Task<UserViewModel> GetUserByEmailAsync(string email)
    {
        var userEntity = await _userRepository.FindOneAsync(user => user.Email == email);
        return _mapper.Map<UserViewModel>(userEntity);
    }

    public async Task<UserViewModel> GetUserByNameAsync(string name)
    {
        var userEntity = await _userRepository.FindOneAsync(user => user.Name == name);
        return _mapper.Map<UserViewModel>(userEntity);
    }

    public async Task<bool> UpdateUserAsync(string id, UserUpdateRequest user)
    {
        var userEnitity = await GetUserByIdAsync(id);

        if (userEnitity is null)
        {
            return false;
        }
        user.Id = userEnitity.Id;
        await _userRepository.ReplaceOneAsync(_mapper.Map<User>(user));
        return true;
    }
}
