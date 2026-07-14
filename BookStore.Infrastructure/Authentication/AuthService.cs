
using Hangfire;
using Microsoft.AspNetCore.Identity.UI.Services;
using SurveyBasket.Api.Helpers;

namespace BookStore.Infrastructure.Authentication;

public class AuthService(UserManager<ApplicationUser> userManager,
    SignInManager<ApplicationUser> signInManager,
    ApplicationDbContext context,
    IJwtProvider jwtProvider,
    IEmailSender emailSender,
    IHttpContextAccessor httpContextAccessor,
    IOptions<RefreshTokenOptions> RefreshTokenOptions
    ) : IAuthService
{
    private readonly UserManager<ApplicationUser> _userManager = userManager;
    private readonly SignInManager<ApplicationUser> _signInManager = signInManager;
    private readonly ApplicationDbContext _context = context;
    private readonly IJwtProvider _jwtProvider = jwtProvider;
    private readonly IEmailSender _emailSender = emailSender;
    private readonly IHttpContextAccessor _httpContextAccessor = httpContextAccessor;
    private readonly RefreshTokenOptions _refreshTokenOptions = RefreshTokenOptions.Value;

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
        var claims = await GetUserClaims(user, cancellationToken);
        var (token, expiresIn) = _jwtProvider.GenerateToken(claims);

        var newRefreshToken = new RefreshToken
        {
            Token = GenerateRefreshToken(),
            ExpireOn = DateTime.UtcNow.AddDays(_refreshTokenOptions.ExpirationInDays)
        };

        user.RefreshTokens.Add(newRefreshToken);
        await _userManager.UpdateAsync(user);

        AuthResponse response =
            new(user.Id, user.Email, user.FirstName, user.LastName, token, expiresIn, newRefreshToken.Token, newRefreshToken.ExpireOn);
        return Result.Success(response);
    }
    public async Task<Result> RegisterAsync(RegisterRequest request, CancellationToken cancellationToken = default)
    {
        bool isEmailExist = await _userManager.Users.AnyAsync(x => x.Email == request.Email, cancellationToken);

        if (isEmailExist)
            return Result.Failure(AuthErrors.DuplicatedEmail);

        var newUser = request.Adapt<ApplicationUser>();

        var result = await _userManager.CreateAsync(newUser, request.Password);

        if (!result.Succeeded)
        {
            var error = result.Errors.First();
            return Result.Failure(new Error(error.Code, error.Description, ErrorType.Validation));
        }

        string token = await _userManager.GenerateEmailConfirmationTokenAsync(newUser);
        string code = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(token));
        await SendConfirmationEmailAsync(newUser, code);

        return Result.Success();
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

    // Private
    private static string GenerateRefreshToken()
    {
        return Convert.ToBase64String(RandomNumberGenerator.GetBytes(64));
    }
    private async Task<List<Claim>> GetUserClaims(ApplicationUser user, CancellationToken cancellationToken)
    {
        var roles = await _userManager.GetRolesAsync(user);

        var permissions = await (

            from role in _context.Roles
            join claim in _context.RoleClaims
            on role.Id equals claim.RoleId
            where roles.Contains(role.Name!) && claim.ClaimType == Permissions.Type
            select claim.ClaimValue

        ).Distinct().ToListAsync(cancellationToken);

        return
        [
            new(JwtRegisteredClaimNames.Sub,user.Id),
            new(JwtRegisteredClaimNames.GivenName,user.FirstName),
            new(JwtRegisteredClaimNames.FamilyName,user.LastName),
            new(JwtRegisteredClaimNames.Email,user.Email!),
            new(JwtRegisteredClaimNames.Jti,Guid.CreateVersion7().ToString()),
            new("Roles",JsonSerializer.Serialize(roles),JsonClaimValueTypes.JsonArray),
            new("Permissions",JsonSerializer.Serialize(permissions),JsonClaimValueTypes.JsonArray),
        ];
    }
    private async Task SendConfirmationEmailAsync(ApplicationUser user, string code)
    {
        var origin = _httpContextAccessor.HttpContext?.Request.Headers.Origin;

        var emailBody = await EmailBodyBuilder.BuildEmailBodyAsync("ConfirmationEmail", new Dictionary<string, string>
        {
            {"{{name}}",user.FirstName},
            {"{{action_url}}",$"{origin}/Auth/confirm-email?userId={user.Id}&code={code}"},
        });

        BackgroundJob.Enqueue(() => _emailSender.SendEmailAsync(user.Email!, "Confirm email at Book Store", emailBody));
    }

}
