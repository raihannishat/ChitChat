using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace ChitChat.Identity.Utilities;
public class TokenHelper : ITokenHelper
{
    private readonly TokenValidationParameters _tokenValidationParameters;
    public TokenHelper(TokenValidationParameters tokenValidationParameters)
    {
        _tokenValidationParameters = tokenValidationParameters;
    }

    public ClaimsPrincipal GetPrincipalFromToken(string token)
    {
        var tokenHandler = new JwtSecurityTokenHandler();
        try
        {
            var principal = tokenHandler.ValidateToken(token, _tokenValidationParameters, out var validateToken);
            if (!IsJwtWithValidSecurityAlgorithm(validateToken))
            {
                return null;
            }
            return principal;
        }
        catch
        {
            return null;
        }
    }

    public bool IsJwtWithValidSecurityAlgorithm(SecurityToken validatedToken)
    {
        return (validatedToken is JwtSecurityToken jwtSecurityToken) &&
            jwtSecurityToken.Header.Alg.Equals(
                SecurityAlgorithms.HmacSha256, StringComparison.InvariantCultureIgnoreCase);
    }

    public string RandomString(int length)
    {
        var random = new Random();
        var chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
        return new string(Enumerable.Repeat(chars, length)
            .Select(x => x[random.Next(x.Length)]).ToArray());
    }
}
