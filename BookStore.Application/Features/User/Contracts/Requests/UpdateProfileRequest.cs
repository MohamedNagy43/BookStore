namespace BookStore.Application.Features.User.Contracts.Requests;

public record UpdateProfileRequest(
    string FirstName,
    string LastName
);
