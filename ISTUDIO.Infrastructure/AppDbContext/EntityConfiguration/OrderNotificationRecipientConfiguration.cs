
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ISTUDIO.Infrastructure.AppDbContext.EntityConfiguration;

public class OrderNotificationRecipientConfiguration : IEntityTypeConfiguration<OrderNotificationRecipientEntity>
{
    public void Configure(EntityTypeBuilder<OrderNotificationRecipientEntity> builder)
    {
        builder.ToTable("OrderNotificationRecipients"); // Устанавливаем имя таблицы в базе данных

        builder.HasKey(e => e.Id); // Устанавливаем первичный ключ

        // Устанавливаем максимальную длину и обязательность для свойств FullName и PhoneNumber
        builder.Property(e => e.FullName)
               .HasMaxLength(250)
               .IsRequired();

        builder.Property(e => e.PhoneNumber)
               .HasMaxLength(20)
               .IsRequired();

        // Устанавливаем уникальный индекс на комбинацию FullName и PhoneNumber, если нужно
        builder.HasIndex(e => new { e.FullName, e.PhoneNumber }).IsUnique();
    }
}
