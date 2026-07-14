using BookStore.Shared.Constants.Identity;

namespace BookStore.Application.Features.Role.Errors;

public static class RoleErrors
{
    public static Error RoleNotFound => new Error("Role.NotFound"
    , "There is no role with this Name", ErrorType.NotFound);

    public static Error DuplicatedRole => new Error("Role.DuplicatedRole"
    , "There is a role with the same Name", ErrorType.Conflict);

    public static Error InvaildPermissions => new Error("Role.InvaildPermissions"
    , $"Permissions must be in the allowed permissions: [{string.Join(',', Permissions.GetAllPermissions())}]", ErrorType.Validation);
    public static Error InvaildRoles => new Error("User.InvaildRoles"
    , $"Roles must be in the allowed Roles", ErrorType.Validation);
}
