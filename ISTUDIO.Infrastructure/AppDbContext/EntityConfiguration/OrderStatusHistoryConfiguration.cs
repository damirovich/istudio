using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ISTUDIO.Infrastructure.AppDbContext.EntityConfiguration;

public class OrderStatusHistoryConfiguration : IEntityTypeConfiguration<OrderStatusHistoryEntity>
{
    public void Configure(EntityTypeBuilder<OrderStatusHistoryEntity> builder)
    {
        builder.ToTable("OrderStatusHistory"); // Устанавливаем имя таблицы в базе данных

        builder.HasKey(e => e.Id); // Устанавливаем первичный ключ

        // Устанавливаем максимальную длину для свойства Status и делаем его обязательным
        builder.Property(e => e.Status)
               .HasMaxLength(250)
               .IsRequired();

        // Устанавливаем связь с таблицей Order (один заказ может иметь много записей истории статусов)
        builder.HasOne(e => e.Order)
               .WithMany(o => o.StatusHistories)
               .HasForeignKey(e => e.OrderId);

        // Устанавливаем свойство ChangeDate как обязательное
        builder.Property(e => e.ChangeDate)
             .HasDefaultValue(DateTime.UtcNow)
           .ValueGeneratedOnAdd();
    }
}
