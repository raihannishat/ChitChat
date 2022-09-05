namespace ChitChat.Identity.Services;

public class AuthService : IAuthService
{
    private readonly string _key;
    private readonly IUserRepository _userRepository;
    private readonly IAuthRepository _authRepository;
    private readonly ITokenHelper _tokenHelper;
    private readonly IMapper _mapper;

    public AuthService(IConfiguration configuration, IAuthRepository authRepository, IUserRepository userRepository,
        IMapper mapper, ITokenHelper tokenHelper, IJwtSettings jwtSettings)
    {
        _authRepository = authRepository;
        _userRepository = userRepository;
        _mapper = mapper;
        _key = jwtSettings.Secret;
        _tokenHelper = tokenHelper;
    }

    public async Task SignUpAsync(UserSignUpDTO user)
    {
        user.Password = SHA_256.ComputeHash(user.Password);

        await _userRepository.InsertOneAsync(_mapper.Map<User>(user));
    }

    public async Task<AuthenticationResult> SignInAsync(UserSignInDTO user)
    {
        var password = SHA_256.ComputeHash(user.Password);
        var existingUser = await _userRepository.FindOneAsync(x => x.Name == user.Name && x.Password == password);

        if (existingUser == null)
        {
            return new AuthenticationResult
            {
                Success = false,
                Errors = new[] { "Invalid username/password" }
            };
        }

        return await GenerateTokenAsync(existingUser);
    }

    public async Task<AuthenticationResult> GenerateTokenAsync(User user)
    {
        var tokenHandlder = new JwtSecurityTokenHandler();

        var tokenKey = Encoding.ASCII.GetBytes(_key);

        var tokenDescriptor = new SecurityTokenDescriptor()
        {
            Subject = new ClaimsIdentity(new Claim[]
            {
                new Claim("Id", user.Id),
                new Claim(JwtRegisteredClaimNames.Sub, user.Name),
                new Claim(JwtRegisteredClaimNames.Email, user.Email),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.UniqueName, user.Name)
            }),

            Expires = DateTime.UtcNow.AddSeconds(60),

            SigningCredentials = new SigningCredentials(
                new SymmetricSecurityKey(tokenKey),
                SecurityAlgorithms.HmacSha256Signature)
        };

        var token = tokenHandlder.CreateToken(tokenDescriptor);

        var refreshToken = new RefreshToken
        {
            Token = _tokenHelper.RandomString(35) + Guid.NewGuid(),
            JwtId = token.Id,
            UserId = user.Id,
            CreationDate = DateTime.UtcNow,
            ExpiryDate = DateTime.UtcNow.AddMonths(6)
        };

        await InsertRefreshTokenAsync(refreshToken);

        return new AuthenticationResult
        {
            Token = tokenHandlder.WriteToken(token),
            RefreshToken = refreshToken.Token,
            Success = true
        };
    }

    public async Task InsertRefreshTokenAsync(RefreshToken token)
    {
        await _authRepository.InsertOneAsync(token);
    }

    public async Task<AuthenticationResult> GetRefreshTokenAsync(string token, string refreshToken)
    {
        var validatedToken = _tokenHelper.GetPrincipalFromToken(token);

        if (validatedToken == null)
        {
            return new AuthenticationResult
            {
                Errors = new[] { "Invalid Token" }
            };
        }

        var expiryDateUnix = long.Parse(validatedToken.Claims.Single(
            x => x.Type == JwtRegisteredClaimNames.Exp).Value);

        var expiryDateTimeUTC = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc)
            .AddSeconds(expiryDateUnix);

        if (expiryDateTimeUTC > DateTime.UtcNow)
        {
            return new AuthenticationResult { Errors = new[] { "This token hasn't expired yet" } };
        }

        var jti = validatedToken.Claims.Single(x => x.Type == JwtRegisteredClaimNames.Jti).Value;

        //var filter = Builders<RefreshToken>.Filter.Eq("Token", refreshToken);

        var storedRefreshToken = await _authRepository.FindOneAsync(x => x.Token == refreshToken);

        if (storedRefreshToken == null)
        {
            return new AuthenticationResult { Errors = new[] { "This refresh token doesn't exist" } };
        }

        if (DateTime.UtcNow > storedRefreshToken.ExpiryDate)
        {
            return new AuthenticationResult { Errors = new[] { "This refresh token has expired" } };
        }

        if (storedRefreshToken.Invalidate)
        {
            return new AuthenticationResult { Errors = new[] { "This refresh token has invalidated" } };
        }

        if (storedRefreshToken.Used)
        {
            return new AuthenticationResult { Errors = new[] { "This refresh token has been used" } };
        }

        if (storedRefreshToken.JwtId != jti)
        {
            return new AuthenticationResult { Errors = new[] { "This refresh token doesn't match the JWT" } };
        }

        storedRefreshToken.Used = true;

        await _authRepository.ReplaceOneAsync(storedRefreshToken);

        //finding user id
        var id = validatedToken.Claims.Single(x => x.Type == "Id").Value;

        var user = await _userRepository.FindByIdAsync(id);

        return await GenerateTokenAsync(user);
    }
}
