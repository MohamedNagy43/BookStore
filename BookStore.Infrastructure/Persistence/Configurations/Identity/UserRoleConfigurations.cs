using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BookStore.Infrastructure.Persistence.Configurations.Identity;

internal class UserRoleConfigurations : IEntityTypeConfiguration<IdentityUserRole<string>>
{
    public void Configure(EntityTypeBuilder<IdentityUserRole<string>> builder)
    {
        // Default Data

        builder.HasData([
            new IdentityUserRole<string>{
                UserId = DefaultUsers.Admin.Id,
                RoleId = DefaultRoles.Admin.Id,
            },

            new IdentityUserRole<string>{
                UserId = DefaultUsers.Customer.Id,
                RoleId = DefaultRoles.Customer.Id,
            },
        ]);
    }
}
