using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ISTUDIO.Infrastructure.AppDbContext.EntityConfiguration;

public class CategoryEntityConfiguration : IEntityTypeConfiguration<CategoryEntity>
{
    public void Configure(EntityTypeBuilder<CategoryEntity> builder)
    {
        builder.ToTable("Categories");

        builder.HasKey(e => e.Id);

        builder.Property(e => e.Name).HasMaxLength(250).IsRequired();
        builder.Property(e => e.Description).HasMaxLength(500).IsRequired(false);
        builder.Property(e => e.ImageUrl).HasMaxLength(500).IsRequired(false);

        builder.HasMany(e => e.SubCategories)
            .WithOne()
            .HasForeignKey(s => s.CategoryId)
            .IsRequired(false);

        builder.HasIndex(e => e.Name).IsUnique();
    }
}