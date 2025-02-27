using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ISTUDIO.Infrastructure.AppDbContext.EntityConfiguration;

public class PaymentMethodEntityConfiguration : IEntityTypeConfiguration<PaymentMethodEntity>
{
    public void Configure(EntityTypeBuilder<PaymentMethodEntity> builder)
    {
        builder.ToTable("PaymentMethods");

        builder.HasKey(e => e.Id);

        builder.Property(e => e.Name).HasMaxLength(250).IsRequired();

        builder.HasOne(e => e.PaymentType)
            .WithMany(p => p.PaymentMethods)
            .HasForeignKey(e => e.PaymentTypeId)
            .IsRequired();

        builder.Property(e => e.IsAvailable)
            .IsRequired()
            .HasDefaultValue(true);
        builder.Property(e => e.IsTechnicalBreak)
            .IsRequired()
            .HasDefaultValue(false);

        builder.HasIndex(e => e.Id).IsUnique();
    }
}
