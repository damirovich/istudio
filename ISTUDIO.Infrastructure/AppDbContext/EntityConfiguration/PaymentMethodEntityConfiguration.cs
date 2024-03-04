using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ISTUDIO.Infrastructure.AppDbContext.EntityConfiguration;

public class PaymentMethodEntityConfiguration : IEntityTypeConfiguration<PaymentMethodEntity>
{
    public void Configure(EntityTypeBuilder<PaymentMethodEntity> builder)
    {
        builder.ToTable("PaymentMethods");

        builder.HasKey(e => e.Id);

        builder.Property(e => e.Value).HasMaxLength(100).IsRequired();

        builder.HasOne(e => e.PaymentType)
            .WithMany(p => p.PaymentMethods)
            .HasForeignKey(e => e.PaymentTypeId)
            .IsRequired();

        builder.HasIndex(e => e.Id).IsUnique();
    }
}