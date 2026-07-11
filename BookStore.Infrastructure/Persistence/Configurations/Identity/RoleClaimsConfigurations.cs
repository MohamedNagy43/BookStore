using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BookStore.Infrastructure.Persistence.Configurations.Identity;

internal class RoleClaimsConfigurations : IEntityTypeConfiguration<IdentityRoleClaim<string>>
{
    public void Configure(EntityTypeBuilder<IdentityRoleClaim<string>> builder)
    {
        int counter = 1;
        var permissions = Permissions.GetAllPermissions().Select(x => new IdentityRoleClaim<string>
        {
            Id = counter++,
            ClaimType = Permissions.Type,
            ClaimValue = x,
            RoleId = DefaultRoles.Admin.Id
        });

        // Default Data
        builder.HasData(permissions);
    }
}
