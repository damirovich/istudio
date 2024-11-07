using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ISTUDIO.Infrastructure.AppDbContext.EntityConfiguration;

public class ProductAttributeEntityConfiguration : IEntityTypeConfiguration<ProductAttributeEntity>
{
    public void Configure(EntityTypeBuilder<ProductAttributeEntity> builder)
    {
        builder.ToTable("ProductAttributes");

        // Определение ключа
        builder.HasKey(e => e.Id);

        // Название атрибута
        builder.Property(e => e.Name)
               .HasMaxLength(100)
               .IsRequired(false);

        // Значение атрибута
        builder.Property(e => e.Value)
               .HasMaxLength(255)
               .IsRequired(false);

        // Связь с вариантом продукта
        builder.HasOne(pa => pa.ProductVariant)
               .WithMany(pv => pv.Attributes)
               .HasForeignKey(pa => pa.ProductVariantId)
               .OnDelete(DeleteBehavior.Cascade);

        builder.HasIndex(e => e.Id).IsUnique();
    }
}