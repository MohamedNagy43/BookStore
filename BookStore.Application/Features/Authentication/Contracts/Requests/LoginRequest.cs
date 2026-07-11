namespace BookStore.Application.Features.Authentication.Contracts.Requests;

public record LoginRequest(string Email, string Password);
