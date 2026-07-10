using Microsoft.EntityFrameworkCore.Metadata.Builders;
namespace BookStore.Infrastructure.Persistence.Configurations.Identity;

internal class RoleConfigurations : IEntityTypeConfiguration<ApplicationRole>
{
    public void Configure(EntityTypeBuilder<ApplicationRole> builder)
    {
        // Default Data
        builder.HasData([
            new ApplicationRole{
                Id = DefaultRoles.Admin.Id,
                Name = DefaultRoles.Admin.Name,
                NormalizedName = DefaultRoles.Admin.Name.ToUpper(),
                ConcurrencyStamp = DefaultRoles.Admin.ConcurrencyStamp,
                IsDefault = DefaultRoles.Admin.IsDefault,
            },    

            new ApplicationRole{
                Id = DefaultRoles.Customer.Id,
                Name = DefaultRoles.Customer.Name,
                NormalizedName = DefaultRoles.Customer.Name.ToUpper(),
                ConcurrencyStamp = DefaultRoles.Customer.ConcurrencyStamp,
                IsDefault = DefaultRoles.Customer.IsDefault,
            },    
        ]);
    }
}
