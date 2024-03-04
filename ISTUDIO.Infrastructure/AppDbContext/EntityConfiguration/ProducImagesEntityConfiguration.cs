using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ISTUDIO.Infrastructure.AppDbContext.EntityConfiguration;

public class ProducImagesEntityConfiguration : IEntityTypeConfiguration<ProducImagesEntity>
{
    public void Configure(EntityTypeBuilder<ProducImagesEntity> builder)
    {
        builder.ToTable("ProductImages");

        builder.HasKey(e => e.Id);

        builder.Property(e => e.TypeImg).HasMaxLength(50).IsRequired();
        builder.Property(e => e.Url).HasMaxLength(500).IsRequired();
        builder.Property(e => e.Name).HasMaxLength(100).IsRequired();
        builder.Property(e => e.ContentType).HasMaxLength(50).IsRequired();

        builder.HasOne(e => e.Products)
            .WithMany(p => p.Images)
            .HasForeignKey(e => e.ProductId)
            .IsRequired();

        builder.HasIndex(e => e.Id).IsUnique();
    }
}