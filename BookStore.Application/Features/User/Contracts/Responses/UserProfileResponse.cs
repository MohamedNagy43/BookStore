namespace BookStore.Application.Features.User.Contracts.Responses;

public record UserProfileResponse(
    string FirstName,
    string LastName,
    string Email,
    string UserName
);
