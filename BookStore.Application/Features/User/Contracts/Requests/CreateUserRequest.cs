namespace BookStore.Application.Features.User.Contracts.Requests;

public record CreateUserRequest(
    string FirstName,
    string LastName,
    string Email,
    IList<string> Roles
);
