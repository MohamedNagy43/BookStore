namespace BookStore.Application.Features.Role.Contracts.Requests;

public record RoleRequest(string Name, IList<string> Permissions);
