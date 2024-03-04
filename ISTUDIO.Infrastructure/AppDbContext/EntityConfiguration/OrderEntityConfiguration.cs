using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ISTUDIO.Infrastructure.AppDbContext.EntityConfiguration;

public class OrderEntityConfiguration : IEntityTypeConfiguration<OrderEntity>
{
    public void Configure(EntityTypeBuilder<OrderEntity> builder)
    {
        builder.ToTable("Orders");

        builder.HasKey(e => e.Id);

        builder.Property(e => e.ShippingAddress).IsRequired();
        builder.Property(e => e.TotalPrice).HasColumnType("decimal(18, 2)").IsRequired();
        builder.Property(e => e.Status).IsRequired();

        builder.HasMany(e => e.Products)
            .WithMany(p => p.Orders)
            .UsingEntity(j => j.ToTable("OrderProducts"));

        builder.HasMany(e => e.Details)
            .WithOne(d => d.Order)
            .HasForeignKey(d => d.OrderId)
            .IsRequired();

        builder.HasOne(e => e.Invoice)
            .WithOne()
            .HasForeignKey<InvoiceEntity>(e => e.OrderId)
            .IsRequired(false);

        builder.HasIndex(e => e.Id).IsUnique();
    }
}
