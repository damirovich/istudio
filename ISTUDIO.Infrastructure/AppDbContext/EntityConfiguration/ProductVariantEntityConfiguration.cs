using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ISTUDIO.Infrastructure.AppDbContext.EntityConfiguration;

public class ProductVariantEntityConfiguration : IEntityTypeConfiguration<ProductVariantEntity>
{
    public void Configure(EntityTypeBuilder<ProductVariantEntity> builder)
    {
        builder.ToTable("ProductVariants");

        // Определение ключа
        builder.HasKey(e => e.Id);

        // Связь с продуктом
        //builder.HasOne(pv => pv.Product)
        //       .WithMany(p => p.ProductVariants)  // Добавьте коллекцию ProductVariants в ProductsEntity, если ее еще нет
        //       .HasForeignKey(pv => pv.ProductId)
        //       .OnDelete(DeleteBehavior.Restrict);

        // Цена
        builder.Property(pv => pv.Price)
               .HasColumnType("decimal(18, 2)")
               .IsRequired();

        // Количество на складе
        builder.Property(pv => pv.QuantityInStock)
               .IsRequired(false);

        // Связь с атрибутами
        builder.HasMany(pv => pv.Attributes)
               .WithOne(a => a.ProductVariant)
               .HasForeignKey(a => a.ProductVariantId)
               .OnDelete(DeleteBehavior.Cascade);

        builder.HasIndex(e => e.Id).IsUnique();
    }
}
