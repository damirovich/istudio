using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ISTUDIO.Infrastructure.AppDbContext.EntityConfiguration;

public class ProductCashbackEntityConfiguration : IEntityTypeConfiguration<ProductCashbackEntity>
{
    public void Configure(EntityTypeBuilder<ProductCashbackEntity> builder)
    {
        // Название таблицы
        builder.ToTable("ProductCashbacks");

        // Уникальный идентификатор
        builder.HasKey(e => e.Id);

        // Связь с продуктом
        builder.HasOne(e => e.Product)
            .WithMany(p => p.ProductCashbacks)
            .HasForeignKey(e => e.ProductId)
            .OnDelete(DeleteBehavior.Cascade); // Удаление кешбэков при удалении продукта

        // Поле MaxBonusPercent
        builder.Property(e => e.MaxBonusPercent)
            .HasColumnType("decimal(5,2)") // Подходит для хранения процентов
            .IsRequired(); // Обязательное поле

        // Индексы для оптимизации запросов
        builder.HasIndex(e => e.ProductId).IsUnique(false); // Несколько записей могут относиться к одному продукту
    }
}
