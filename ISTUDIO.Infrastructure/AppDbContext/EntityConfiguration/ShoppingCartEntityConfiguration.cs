using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ISTUDIO.Infrastructure.AppDbContext.EntityConfiguration;

public class ShoppingCartEntityConfiguration : IEntityTypeConfiguration<ShoppingCartEntity>
{
    public void Configure(EntityTypeBuilder<ShoppingCartEntity> builder)
    {
        builder.ToTable("ShoppingCarts");

        builder.HasKey(e => e.Id);

        builder.Property(e => e.UserId).IsRequired();
        builder.Property(e => e.QuantyProduct).IsRequired();
        builder.Property(e => e.CreateDate).IsRequired(false);

        // Определяем связь многие ко многим с сущностью ProductsEntity
        builder.HasMany(e => e.Products)
               .WithMany(p => p.ShoppingCarts)
               .UsingEntity(j => j.ToTable("ShoppingCartProducts"));
    }
}
