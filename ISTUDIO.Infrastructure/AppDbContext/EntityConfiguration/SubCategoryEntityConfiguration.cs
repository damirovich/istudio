using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ISTUDIO.Infrastructure.AppDbContext.EntityConfiguration;

public class SubCategoryEntityConfiguration : IEntityTypeConfiguration<SubCategoryEntity>
{
    public void Configure(EntityTypeBuilder<SubCategoryEntity> builder)
    {
        builder.ToTable("SubCategories");

        builder.HasKey(e => e.Id);

        builder.Property(e => e.Name).HasMaxLength(250).IsRequired();
        builder.Property(e => e.Description).HasMaxLength(500).IsRequired(false);
        builder.Property(e => e.ImageUrl).HasMaxLength(500).IsRequired(false);

        builder.HasOne(e => e.Category)
            .WithMany(c => c.SubCategories)
            .HasForeignKey(e => e.CategoryId)
            .IsRequired(false);

        builder.HasIndex(e => e.Name).IsUnique();
    }
}