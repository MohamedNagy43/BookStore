using BookStore.Application.Features.Authentication.Errors;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;

namespace BookStore.Infrastructure.Authentication;

public class AuthService(UserManager<ApplicationUser> userManager,
    SignInManager<ApplicationUser> signInManager,
    IJwtProvider jwtProvider
    ) : IAuthService
{
    private readonly UserManager<ApplicationUser> _userManager = userManager;
    private readonly SignInManager<ApplicationUser> _signInManager = signInManager;
    private readonly IJwtProvider _jwtProvider = jwtProvider;

    public async Task<Result<AuthResponse>> GetTokenAsync(LoginRequest request, CancellationToken cancellationToken = default)
    {
        // Validation
        if (await _userManager.FindByEmailAsync(request.Email) is not { } user)
            return Result.Failure<AuthResponse>(AuthErrors.InvalidCredentials);

        if (user.IsDisabled)
            return Result.Failure<AuthResponse>(AuthErrors.AccountDisabled);

        var result = await _signInManager.PasswordSignInAsync(user, request.Password, false, true);

        if (result.IsLockedOut)
            return Result.Failure<AuthResponse>(AuthErrors.AccountLockedOut);

        if (result.IsNotAllowed)
            return Result.Failure<AuthResponse>(AuthErrors.EmailNotConfirmed);

        if (!result.Succeeded)
            return Result.Failure<AuthResponse>(AuthErrors.InvalidCredentials);

        // Generate Token
        //var (roles, permissions) = await GetUserRolesAndPermissions(user, cancellationToken);
        //IEnumerable<Claim> claims = [];
        //var (token, expiresIn) = _jwtProvider.GenerateToken(claims);

        //var newRefreshToken = new RefreshToken(_refreshTokenExpirationDays);
        //user.RefreshTokens.Add(newRefreshToken);
        //await _userManager.UpdateAsync(user);

        //return Result.Success(new AuthResponse(user.Id, user.Email, user.FirstName, user.LastName, accesstoken, expiresIn, newRefreshToken.Token, newRefreshToken.ExpiresOn));

        throw new NotImplementedException();

    }
    public Task<Result> RegisterAsync(RegisterRequest request, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }
    public Task<Result> GetRefreshTokenAsync(string token, string refreshToken, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }
    public Task<Result> RevokeRefreshTokenAsync(string token, string refreshToken, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }
    public Task<Result> ResendEmailConfirmationCodeAsync(string email, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }
    public Task<Result> ConfirmEmailAsync(ConfirmEmailRequest request)
    {
        throw new NotImplementedException();
    }
    public Task<Result> SendResetPasswordCodeAsync(string email)
    {
        throw new NotImplementedException();
    }
    public Task<Result> ResetPasswordAsync(ResetPasswordRequest request)
    {
        throw new NotImplementedException();
    }

    // Helpers
    private async Task<(IEnumerable<string> roles, IEnumerable<string> permissions)> GetUserRolesAndPermissions(ApplicationUser user, CancellationToken cancellationToken)
    {
        var roles = await _userManager.GetRolesAsync(user);
        // TODO Permissions
        //var userRoles = await _userManager.GetRolesAsync(user);

        //var permissions = await (

        //    from role in _context.Roles
        //    join claim in _context.RoleClaims
        //    on role.Id equals claim.RoleId
        //    where userRoles.Contains(role.Name!) && claim.ClaimType == Permissions.Type
        //    select claim.ClaimValue

        //).Distinct().ToListAsync(cancellationToken);

        return (roles, null)!;
    }

}
