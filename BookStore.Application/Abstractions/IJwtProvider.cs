using System.Security.Claims;

namespace BookStore.Application.Abstractions;

public interface IJwtProvider
{
    (string token, int expiresIn) GenerateToken(IEnumerable<Claim> claims);

    /// <summary>
    /// Return Userid of valid access token,return null if validation fails
    /// </summary>
    /// <param name="token"></param>
    /// <returns></returns>
    string? ValidateToken(string token, bool validateLifeTime = true);
}