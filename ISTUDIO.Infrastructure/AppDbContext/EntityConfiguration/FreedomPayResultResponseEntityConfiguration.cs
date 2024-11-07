using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ISTUDIO.Infrastructure.AppDbContext.EntityConfiguration;

public class FreedomPayResultResponseEntityConfiguration : IEntityTypeConfiguration<FreedomPayResultResponseEntity>
{
    public void Configure(EntityTypeBuilder<FreedomPayResultResponseEntity> builder)
    {
        // Указываем таблицу, к которой привязана сущность
        builder.ToTable("FreedomPayResultResponses");

        // Указываем ключ
        builder.HasKey(e => e.Id);

        // Конфигурация свойств
        builder.Property(e => e.Status).IsRequired().HasMaxLength(50);

        builder.Property(e => e.Description).HasMaxLength(500);

        builder.Property(e => e.Salt).IsRequired().HasMaxLength(500);

        builder.Property(e => e.Sig).IsRequired().HasMaxLength(500);

        builder.Property(e => e.CreatedDate)
            .IsRequired()
            .HasDefaultValueSql("GETDATE()"); // SQL Server функция для текущей даты
    }
}
