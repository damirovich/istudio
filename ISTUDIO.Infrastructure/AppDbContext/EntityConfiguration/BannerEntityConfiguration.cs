using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ISTUDIO.Infrastructure.AppDbContext.EntityConfiguration;

public class BannerEntityConfiguration : IEntityTypeConfiguration<BannerEntity>
{
    public void Configure(EntityTypeBuilder<BannerEntity> builder)
    {
        builder.ToTable("Banners");

        builder.HasKey(e => e.Id);

        builder.Property(e => e.PhotoUrl).HasMaxLength(500).IsRequired();
        builder.Property(e => e.Status).IsRequired();
     
        builder.HasOne(e => e.Categories)
              .WithMany(c => c.Baners)
              .HasForeignKey(e => e.CategoryId)
              .IsRequired(false);
        
        builder.HasOne(e => e.Discounts)
              .WithMany(c => c.Baners)
              .HasForeignKey(e => e.DiscountId)
              .IsRequired(false);

        builder.HasOne(e => e.Products)
              .WithMany(c => c.Baners)
              .HasForeignKey(e => e.ProductId)
              .IsRequired(false);

        builder.HasIndex(e => e.Id).IsUnique();
    }
}
