namespace BookStore.Application.Features.User.Contracts.Requests;

public record UpdateUserRequest(
    string FirstName,
    string LastName,
    string Email,
    IList<string> Roles
);
