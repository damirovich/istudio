using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ISTUDIO.Infrastructure.AppDbContext.EntityConfiguration;

public class CashbackEntityConfiguration : IEntityTypeConfiguration<CashbackEntity>
{
    public void Configure(EntityTypeBuilder<CashbackEntity> builder)
    {
        builder.ToTable("Cashbacks");

        builder.HasKey(e => e.Id);

        builder.Property(e => e.CashbackPercent)
            .HasColumnType("decimal(5,2)")
            .IsRequired();

        builder.Property(e => e.StartDate)
            .IsRequired();

        builder.Property(e => e.EndDate)
            .IsRequired();

        builder.Property(e => e.IsActive)
            .IsRequired();

        // Добавляем связь с кешбэком
        builder.HasMany(p => p.Products)
               .WithOne(pc => pc.Cashback)
               .HasForeignKey(pc => pc.CashbackId);

    }
}
