using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ISTUDIO.Infrastructure.AppDbContext.EntityConfiguration;

public class OrderDeliveryEntityConfiguration : IEntityTypeConfiguration<OrderDeliveryEntity>
{
    public void Configure(EntityTypeBuilder<OrderDeliveryEntity> builder)
    {
        builder.ToTable("OrderDeliveries");

        builder.HasKey(e => e.Id);

        builder.Property(e => e.DeliveryMethod).HasMaxLength(100).IsRequired();
        builder.Property(e => e.Status).HasMaxLength(250).IsRequired();
        builder.Property(e => e.TrackingNumber).HasMaxLength(100);
        builder.Property(e => e.EstimatedDate).IsRequired(false);
        builder.Property(e => e.DeliveredDate).IsRequired(false);

        builder.HasOne(e => e.Order)
            .WithMany(o => o.Deliveries)
            .HasForeignKey(e => e.OrderId)
            .IsRequired();

        builder.HasOne(e => e.OrderAddress)
            .WithMany()
            .HasForeignKey(e => e.OrderAddressId)
            .IsRequired();

        builder.HasIndex(e => e.TrackingNumber).IsUnique(false);
    }
}
