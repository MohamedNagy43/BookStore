using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

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
