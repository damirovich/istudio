
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ISTUDIO.Infrastructure.AppDbContext.EntityConfiguration;

public class ProductsEntityConfiguration : IEntityTypeConfiguration<ProductsEntity>
{
    public void Configure(EntityTypeBuilder<ProductsEntity> builder)
    {
        builder.ToTable("Products");

        builder.HasKey(e => e.Id);

        builder.Property(e => e.Name).HasMaxLength(255).IsRequired();
        builder.Property(e => e.Model).HasMaxLength(100).IsRequired();
        builder.Property(e => e.Color).HasMaxLength(50).IsRequired();
        builder.Property(e => e.Price).HasColumnType("decimal(18, 2)").IsRequired();
        builder.Property(e => e.QuantityInStock).IsRequired();
        builder.Property(e => e.Description).IsRequired();

        builder.HasOne(e => e.Category)
            .WithMany(c => c.Products)
            .HasForeignKey(e => e.CategoryId)
            .IsRequired();

        builder.HasMany(e => e.Images)
            .WithOne(i => i.Products)
            .HasForeignKey(i => i.ProductId)
            .IsRequired();

        builder.HasOne(e => e.Discount)
            .WithOne()
            .HasForeignKey<DiscountEntity>(d => d.ProductId)
            .IsRequired(false);


        builder.HasIndex(e => e.Id).IsUnique();
    }
}