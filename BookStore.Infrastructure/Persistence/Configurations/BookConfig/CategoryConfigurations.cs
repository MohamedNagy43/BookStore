using BookStore.Domain.Entities.Categories;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace BookStore.Infrastructure.Persistence.Configurations.BookConfig;

public class CategoryConfigurations : IEntityTypeConfiguration<Category>
{
    public void Configure(EntityTypeBuilder<Category> builder)
    {
        builder.Property(x => x.Name).HasMaxLength(250);
        builder.HasIndex(x => x.Name).IsUnique();
        builder.Property(x => x.Description).HasMaxLength(600);
    }
}
