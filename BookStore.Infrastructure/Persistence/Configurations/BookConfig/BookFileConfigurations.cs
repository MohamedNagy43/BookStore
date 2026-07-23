using BookStore.Domain.Entities.Books;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BookStore.Infrastructure.Persistence.Configurations.BookConfig;

public class BookFileConfigurations : IEntityTypeConfiguration<BookFile>
{
    public void Configure(EntityTypeBuilder<BookFile> builder)
    {
        builder.HasKey(x => new { x.BookId, x.FileId });
        builder.Property(x => x.DisplayOrder).HasMaxLength(10);
        builder.Property(x => x.IsPrimary).HasDefaultValue(false);

        builder.HasOne(x => x.Book)
            .WithMany(x => x.BookFiles)
            .HasForeignKey(x => x.BookId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(x => x.File)
            .WithMany()
            .HasForeignKey(x => x.FileId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}
