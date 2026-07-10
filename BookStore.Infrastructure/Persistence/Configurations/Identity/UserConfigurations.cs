using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BookStore.Infrastructure.Persistence.Configurations.Identity;

internal class UserConfigurations : IEntityTypeConfiguration<ApplicationUser>
{
    public void Configure(EntityTypeBuilder<ApplicationUser> builder)
    {
        builder.Property(x => x.FirstName).HasMaxLength(100);
        builder.Property(x => x.LastName).HasMaxLength(100);

        //Default Data

        builder.HasData([
            new ApplicationUser{
                Id = DefaultUsers.Admin.Id,
                FirstName = DefaultUsers.Admin.FirstName,
                LastName = DefaultUsers.Admin.LastName,
                Email = DefaultUsers.Admin.Email,
                NormalizedEmail = DefaultUsers.Admin.Email.ToUpper(),
                UserName = DefaultUsers.Admin.Email.Split('@')[0],
                NormalizedUserName = DefaultUsers.Admin.Email.Split('@')[0].ToUpper(),
                ConcurrencyStamp = DefaultUsers.Admin.ConcurrencyStamp,
                SecurityStamp = DefaultUsers.Admin.SecurityStamp,
                PasswordHash = DefaultUsers.Admin.PasswordHashed,
                EmailConfirmed = true,
            },

            new ApplicationUser{
                Id = DefaultUsers.Customer.Id,
                FirstName = DefaultUsers.Customer.FirstName,
                LastName = DefaultUsers.Customer.LastName,
                Email = DefaultUsers.Customer.Email,
                NormalizedEmail = DefaultUsers.Customer.Email.ToUpper(),
                UserName = DefaultUsers.Customer.Email.Split('@')[0],
                NormalizedUserName = DefaultUsers.Customer.Email.Split('@')[0].ToUpper(),
                ConcurrencyStamp = DefaultUsers.Customer.ConcurrencyStamp,
                SecurityStamp = DefaultUsers.Customer.SecurityStamp,
                PasswordHash = DefaultUsers.Customer.PasswordHashed,
                EmailConfirmed = true,
            }
        ]);

        builder.OwnsMany(x => x.RefreshTokens, ownedBuilder =>
        {
            ownedBuilder.ToTable("RefreshTokens");
            ownedBuilder.WithOwner().HasForeignKey("UserId");
            ownedBuilder.Property<int>("Id"); // shadow Property
            ownedBuilder.HasKey("Id", "UserId");
        });
    }
}
