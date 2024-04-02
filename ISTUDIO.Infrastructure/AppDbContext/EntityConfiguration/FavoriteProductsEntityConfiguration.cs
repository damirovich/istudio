using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ISTUDIO.Infrastructure.AppDbContext.EntityConfiguration;

public class FavoriteProductsEntityConfiguration : IEntityTypeConfiguration<FavoriteProductsEntity>
{
    public void Configure(EntityTypeBuilder<FavoriteProductsEntity> builder)
    {
        builder.ToTable("FavoriteProducts"); // Устанавливаем имя таблицы

        builder.HasKey(e => e.Id); // Устанавливаем первичный ключ

        builder.Property(e => e.UserId).IsRequired();

        // Определяем связь многие ко многим с сущностью ProductsEntity
        builder.HasMany(e => e.Products)
               .WithMany(p => p.FavoriteProducts)
               .UsingEntity(j => j.ToTable("UserFavoritesItems"));

        builder.HasIndex(e => e.Id).IsUnique();
    }
}
