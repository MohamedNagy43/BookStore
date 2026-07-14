namespace BookStore.Application.Features.User.Contracts.Requests;

public record ConfirmEmailAndSetPasswordRequest(
    string UserId,    
    string Code,
    string Password
);