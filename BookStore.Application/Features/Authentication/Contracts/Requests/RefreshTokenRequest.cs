namespace BookStore.Application.Features.Authentication.Contracts.Requests;

public record RefreshTokenRequest(
    string Token,
    string RefreshToken
);
