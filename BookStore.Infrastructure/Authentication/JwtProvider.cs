using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace BookStore.Infrastructure.Authentication;

public class JwtProvider(IOptions<JwtOptions> jwtOption) : IJwtProvider
{
    private readonly JwtOptions _jwtOptions = jwtOption.Value;
    public (string token, int expiresIn) GenerateToken(IEnumerable<Claim> claims)
    {
        // signingCredentials
        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtOptions.SecretKey));
        var signingCredentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);


        var jwtSecurityToken = new JwtSecurityToken
        (
            issuer: _jwtOptions.Issuer,
            audience: _jwtOptions.Audience,
            expires: DateTime.UtcNow.AddMinutes(_jwtOptions.ExpiryMinutes),
            claims: claims,
            signingCredentials: signingCredentials
        );

        var token = new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken);

        return (token, _jwtOptions.ExpiryMinutes * 60);

    }

    public string? ValidateToken(string token, bool validateLifeTime = true)
    {
        var tokenHandler = new JwtSecurityTokenHandler();
        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtOptions.SecretKey));

        try
        {
            tokenHandler.ValidateToken(token, new TokenValidationParameters
            {
                ValidateIssuer = false,
                ValidateAudience = false,
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = key,
                ValidateLifetime = validateLifeTime,
                ClockSkew = TimeSpan.Zero
            }, out SecurityToken validatedToken);

            var SecurityToken = (JwtSecurityToken)validatedToken;

            return SecurityToken.Claims.First(c => c.Type == JwtRegisteredClaimNames.Sub).Value;
        }
        catch
        {
            return null;
        }

    }
}
