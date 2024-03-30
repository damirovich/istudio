
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

        // Определение связи с категорией
        builder.HasOne(p => p.Category)
               .WithMany(c => c.Products)
               .HasForeignKey(p => p.CategoryId)
               .OnDelete(DeleteBehavior.Restrict);


        // Определяем внешний ключ для связи со скидкой
        builder.HasOne(p => p.Discount)
            .WithMany(d => d.Products)
            .HasForeignKey(p => p.DiscountId)
             .IsRequired(false)
            .OnDelete(DeleteBehavior.Restrict); // Устанавливаем ограничение на удаление каскадом


        //// Определение связи с изображениями
        builder.HasMany(p => p.Images)
               .WithOne(pi => pi.Products)
               .HasForeignKey(pi => pi.ProductId)
                .IsRequired()
               .OnDelete(DeleteBehavior.Cascade);

        // Определение связи с заказами
        builder.HasMany(o => o.Orders)
             .WithMany(p => p.Products)
             .UsingEntity(j => j.ToTable("OrderProducts"));

        builder.HasMany(e => e.ShoppingCarts)
               .WithMany(p => p.Products)
               .UsingEntity(j => j.ToTable("ShoppingCartProducts"));

        builder.HasIndex(e => e.Id).IsUnique();
    }
}