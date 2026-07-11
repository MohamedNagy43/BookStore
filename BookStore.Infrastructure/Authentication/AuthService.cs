namespace BookStore.Infrastructure.Authentication;
public class AuthService : IAuthService
{
    public Task<Result<AuthResponse>> GetTokenAsync(LoginRequest request, CancellationToken cancellationToken = default)
    {
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
}
