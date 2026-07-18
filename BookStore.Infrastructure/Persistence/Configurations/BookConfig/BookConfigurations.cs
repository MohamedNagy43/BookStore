using BookStore.Domain.Entities.Book;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BookStore.Infrastructure.Persistence.Configurations.BookConfig;

public class BookConfigurations : IEntityTypeConfiguration<Book>
{
    public void Configure(EntityTypeBuilder<Book> builder)
    {
        builder.HasIndex(x=>x.Title).IsUnique();
        builder.Property(x=>x.Title).HasMaxLength(250);
        builder.Property(x => x.Description).HasMaxLength(4000);
        builder.Property(x => x.Edition).HasMaxLength(50);
        builder.Property(x => x.PublisherName).HasMaxLength(250);
        builder.Property(x => x.Language).HasMaxLength(50);
        builder.Property(x=>x.Price).HasColumnType("decimal(18,2)");
        builder.Property(x => x.Weight).HasPrecision(8, 2);
        builder.Property(x => x.IsAvailable).HasDefaultValue(true);

        builder.HasOne(x => x.Author)
          .WithMany(x => x.Books)
          .HasForeignKey(x => x.AuthorId)
          .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(x => x.Category)
            .WithMany(x => x.Books)
            .HasForeignKey(x => x.CategoryId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasMany(x => x.BookFiles)
            .WithOne(x => x.Book)
            .HasForeignKey(x => x.BookId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
