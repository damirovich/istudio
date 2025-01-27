using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ISTUDIO.Infrastructure.AppDbContext.EntityConfiguration;

public class OrderPaymentEntityConfiguration : IEntityTypeConfiguration<OrderPaymentEntity>
{
    public void Configure(EntityTypeBuilder<OrderPaymentEntity> builder)
    {
        builder.ToTable("OrderPayments");

        builder.HasKey(e => e.Id);

        builder.Property(e => e.Amount).HasColumnType("decimal(18, 2)").IsRequired();
        builder.Property(e => e.Status).HasMaxLength(250).IsRequired();
        builder.Property(e => e.PaymentDate).IsRequired();
        builder.Property(e => e.TransactionId).HasMaxLength(500);

        builder.HasOne(e => e.Order)
            .WithMany(o => o.Payments)
            .HasForeignKey(e => e.OrderId)
            .IsRequired();

        builder.HasOne(e => e.PaymentMethod)
            .WithMany(p => p.OrderPayments)
            .HasForeignKey(e => e.PaymentMethodId)
            .IsRequired();

        builder.HasIndex(e => e.TransactionId).IsUnique(false);
    }
}
