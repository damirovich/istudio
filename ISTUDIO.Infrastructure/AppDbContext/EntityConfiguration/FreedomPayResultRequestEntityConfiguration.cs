using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ISTUDIO.Infrastructure.AppDbContext.EntityConfiguration;

public class FreedomPayResultRequestEntityConfiguration : IEntityTypeConfiguration<FreedomPayResultRequestEntity>
{
    public void Configure(EntityTypeBuilder<FreedomPayResultRequestEntity> builder)
    {
        // Указываем таблицу, к которой привязана сущность
        builder.ToTable("FreedomPayResultRequests");

        // Указываем ключ
        builder.HasKey(e => e.Id);

        // Конфигурация свойств
        builder.Property(e => e.JsonData).IsRequired(false);
        //builder.Property(e => e.PgOrderId).IsRequired();

        //builder.Property(e => e.PgPaymentId).IsRequired();

        //builder.Property(e => e.PgAmount).IsRequired().HasColumnType("decimal(18, 2)");

        //builder.Property(e => e.PgCurrency).IsRequired().HasMaxLength(10);

        //builder.Property(e => e.PgNetAmount).IsRequired().HasColumnType("decimal(18, 2)");

        //builder.Property(e => e.PgPsAmount).IsRequired().HasColumnType("decimal(18, 2)");

        //builder.Property(e => e.PgPsFullAmount).IsRequired().HasColumnType("decimal(18, 2)");

        //builder.Property(e => e.PgPsCurrency).IsRequired().HasMaxLength(10);

        //builder.Property(e => e.PgDescription).HasMaxLength(500);

        //builder.Property(e => e.PgResult).IsRequired();

        //builder.Property(e => e.PgPaymentDate).IsRequired();

        //builder.Property(e => e.PgCanReject).IsRequired();

        //builder.Property(e => e.PgUserPhone).HasMaxLength(20);
        //builder.Property(e=>e.PgNeedPhoneNotification).IsRequired();

        //builder.Property(e => e.PgUserContactEmail).HasMaxLength(100);
        //builder.Property(e=>e.PgNeedEmailNotification).IsRequired();

        //builder.Property(e => e.PgTestingMode).IsRequired();

        //builder.Property(e => e.PgCaptured).IsRequired();

        //builder.Property(e => e.PgReference).HasMaxLength(100);

        //builder.Property(e => e.PgCardPan).HasMaxLength(50);

        //builder.Property(e => e.PgSalt).IsRequired().HasMaxLength(200);

        //builder.Property(e => e.PgSig).IsRequired().HasMaxLength(500);

        //builder.Property(e => e.PgPaymentMethod).HasMaxLength(50);
        //builder.Property(e => e.PgAuthCode).HasMaxLength(500);

        builder.Property(e => e.CreatedDate)
            .IsRequired()
            .HasDefaultValueSql("GETDATE()"); // SQL Server функция для текущей даты
    }
}
