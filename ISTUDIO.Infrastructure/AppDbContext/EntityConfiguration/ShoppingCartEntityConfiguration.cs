using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ISTUDIO.Infrastructure.AppDbContext.EntityConfiguration;

public class ShoppingCartEntityConfiguration : IEntityTypeConfiguration<ShoppingCartEntity>
{
    public void Configure(EntityTypeBuilder<ShoppingCartEntity> builder)
    {
        builder.ToTable("ShoppingCarts");

        builder.HasKey(e => e.Id);

        builder.Property(e => e.UserId).HasMaxLength(500).IsRequired();

        // Определение связи с деталями заказа
        builder.HasMany(e => e.OrderDetails)
               .WithOne()
               .IsRequired();
    }
}
