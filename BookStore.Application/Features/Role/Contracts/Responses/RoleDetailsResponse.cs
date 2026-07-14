namespace BookStore.Application.Features.Role.Contracts.Responses;

public record RoleDetailsResponse(string Id, string Name, bool IsDeleted, IEnumerable<string> Permissions);
