namespace BookStore.Application.Features.Authentication.Contracts.Requests;

public record ConfirmEmailRequest(
    string UserId,
    string Code
);
