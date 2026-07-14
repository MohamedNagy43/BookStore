using System.Security.Claims;

namespace BookStore.Api.Extension;

public static class UserExtension
{
    public static string? GetUserId(this ClaimsPrincipal User)
        => User.FindFirstValue(ClaimTypes.NameIdentifier);
}
