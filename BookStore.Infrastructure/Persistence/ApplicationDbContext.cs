using BookStore.Application.Abstractions.Common;
using BookStore.Domain.Common;
using BookStore.Domain.Entities.Authors;
using BookStore.Domain.Entities.Books;
using BookStore.Domain.Entities.Categories;
using System.Reflection;

namespace BookStore.Infrastructure.Persistence;

public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options, ICurrentUserService currentUserService)
    : IdentityDbContext<ApplicationUser, ApplicationRole, string>(options), IAppDbContext
{
    private readonly ICurrentUserService _currentUserService = currentUserService;

    public DbSet<Book> Books { get; set; }
    public DbSet<Author> Authors { get; set; }
    public DbSet<Category> Categories { get; set; }
    public DbSet<BookFile> BookFiles { get; set; }
    public DbSet<StoredFile> StoredFiles { get; set; }
    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        base.OnModelCreating(builder);
    }
    public override int SaveChanges()
    {
        AddAuditInformaiton();
        return base.SaveChanges();
    }
    public override Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = default)
    {
        AddAuditInformaiton();
        return base.SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken);
    }
    private void AddAuditInformaiton()
    {
        var AuditableEntries = ChangeTracker
            .Entries<AuditableEntity>()
            .Where(e => e.State == EntityState.Added || e.State == EntityState.Modified);

        string? userId = _currentUserService.UserId ??
             throw new InvalidOperationException("User id must not be null here");

        foreach (var entry in AuditableEntries)
        {
            if (entry.State == EntityState.Added)
            {
                entry.Property(e => e.CreatedById).CurrentValue = userId!;
                entry.Property(e => e.CreatedOn).CurrentValue = DateTime.UtcNow;

            }
            else if (entry.State == EntityState.Modified)
            {

                entry.Property(e => e.CreatedOn).IsModified = false;
                entry.Property(e => e.CreatedById).IsModified = false;

                entry.Property(e => e.UpdatedOn).CurrentValue = DateTime.UtcNow;
                entry.Property(e => e.UpdatedById).CurrentValue = userId;

                if (entry.Property(e => e.IsDeleted).IsModified)
                {
                    if (entry.Property(e => e.IsDeleted).CurrentValue)
                    {
                        entry.Property(e => e.DeletedById).CurrentValue = userId;
                        entry.Property(e => e.DeletedOn).CurrentValue = DateTime.UtcNow;
                    }
                    else
                    {
                        entry.Property(e => e.DeletedById).CurrentValue = null;
                        entry.Property(e => e.DeletedOn).CurrentValue = null;
                    }
                }
            }
        }
    }
}
