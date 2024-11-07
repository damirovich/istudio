using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ISTUDIO.Infrastructure.AppDbContext.EntityConfiguration;

public class FreedomPayRequestEntityConfiguration : IEntityTypeConfiguration<FreedomPayInitRequestEntity>
{
    public void Configure(EntityTypeBuilder<FreedomPayInitRequestEntity> builder)
    {
        // Указываем таблицу, к которой привязана сущность
        builder.ToTable("FreedomInitPayRequests");

        // Указываем ключ
        builder.HasKey(e => e.Id);

        // Конфигурация свойств
        builder.Property(e => e.PgOrderId).IsRequired();

        builder.Property(e => e.PgMerchantId).IsRequired();

        builder.Property(e => e.PgAmount).IsRequired().HasColumnType("decimal(18, 2)"); // Пример формата для decimal

        builder.Property(e => e.PgDescription).HasMaxLength(500); // Максимальная длина описания

        builder.Property(e => e.PgSalt).IsRequired().HasMaxLength(200);

        builder.Property(e => e.PgSig).IsRequired().HasMaxLength(500);

        builder.Property(e => e.CreatedDate)
            .IsRequired()
            .HasDefaultValueSql("GETDATE()"); // SQL Server функция для текущей даты

        // Дополнительные индексы, если необходимо
        //builder.HasIndex(e => e.PgOrderId).IsUnique();
    }
}