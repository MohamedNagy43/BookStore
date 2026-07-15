using Microsoft.AspNetCore.Authorization;

namespace BookStore.Infrastructure.Authentication.Filters;

public class PermissionRequriement(string permission) : IAuthorizationRequirement
{
    public string Permission { get; } = permission;
}
