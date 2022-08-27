namespace ChitChat.Identity.Utilities;

public interface ITokenHelper
{
    ClaimsPrincipal GetPrincipalFromToken(string token);
    bool IsJwtWithValidSecurityAlgorithm(SecurityToken validatedToken);
    string RandomString(int length);
}