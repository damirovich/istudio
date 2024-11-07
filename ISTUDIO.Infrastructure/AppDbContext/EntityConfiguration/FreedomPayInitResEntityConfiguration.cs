using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ISTUDIO.Infrastructure.AppDbContext.EntityConfiguration;

public class FreedomPayInitResEntityConfiguration : IEntityTypeConfiguration<FreedomPayInitResEntity>
{
    public void Configure(EntityTypeBuilder<FreedomPayInitResEntity> builder)
    {
        // Указываем таблицу, к которой привязана сущность
        builder.ToTable("FreedomInitPayResponses");

        // Указываем ключ
        builder.HasKey(e => e.Id);

        // Конфигурация свойств
        builder.Property(e => e.Status).IsRequired().HasMaxLength(50);

        builder.Property(e => e.PaymentId).IsRequired().HasMaxLength(100);

        builder.Property(e => e.RedirectUrl).HasMaxLength(500);

        builder.Property(e => e.RedirectUrlType).HasMaxLength(50);

        builder.Property(e => e.Salt).IsRequired().HasMaxLength(200);

        builder.Property(e => e.Sig).IsRequired().HasMaxLength(500);

        builder.Property(e => e.ResultUrl).HasMaxLength(500);

        builder.Property(e => e.CreatedDate)
            .IsRequired()
            .HasDefaultValueSql("GETDATE()"); // SQL Server функция для текущей даты

        // Дополнительные индексы, если необходимо
      //  builder.HasIndex(e => e.PaymentId).IsUnique();
    }
}
