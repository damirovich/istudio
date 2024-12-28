using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ISTUDIO.Infrastructure.AppDbContext.EntityConfiguration;

public class AppUpdateInfoEntityConfiguration : IEntityTypeConfiguration<AppUpdateInfoEntity>
{
    public void Configure(EntityTypeBuilder<AppUpdateInfoEntity> builder)
    {
        builder.ToTable("AppUpdateInfo");
        // Установка первичного ключа
        builder.HasKey(e => e.Id);

        // Конфигурация свойств
        builder.Property(e => e.LatestVersion)
            .IsRequired()
            .HasMaxLength(50); // Если версия имеет ограничение длины, можно указать

        builder.Property(e => e.UpdateRequired)
            .IsRequired();

        builder.Property(e => e.UpdateUrl)
            .IsRequired()
            .HasMaxLength(255); // Рекомендуется ограничить длину URL

        builder.Property(e => e.Platform)
            .IsRequired()
            .HasMaxLength(255);

        builder.Property(e => e.CreatedDate)
            .IsRequired()
            .HasDefaultValueSql("GETDATE()"); // Использование SQL-функции для текущей даты

        // Индексирование
        //builder.HasIndex(e => e.Id).IsUnique(); // Не обязательно, если это Primary Key
    }

}