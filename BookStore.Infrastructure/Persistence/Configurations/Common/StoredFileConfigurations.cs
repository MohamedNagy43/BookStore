using BookStore.Domain.Common;
using BookStore.Shared.Constants;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BookStore.Infrastructure.Persistence.Configurations.Common;

public class StoredFileConfigurations : IEntityTypeConfiguration<StoredFile>
{
    public void Configure(EntityTypeBuilder<StoredFile> builder)
    {
        builder.Property(x => x.FileName).HasMaxLength(250);
        builder.Property(x => x.StoredFileName).HasMaxLength(250);
        builder.Property(x => x.ContentType).HasMaxLength(50);
        builder.Property(x => x.FileExtension).HasMaxLength(5);
        builder.Property(x => x.Size).HasMaxLength(FileSettings.MaxFileSizeInBytes);
    }
}
