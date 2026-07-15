using Microsoft.AspNetCore.Authorization;

namespace BookStore.Infrastructure.Authentication.Filters;

public class HasPermissionAttribute(string permission) : AuthorizeAttribute(permission)
{
}
