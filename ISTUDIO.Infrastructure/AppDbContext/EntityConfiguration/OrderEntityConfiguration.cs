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
        builder.Property(e => e.TotalQuantyProduct).IsRequired();
        builder.Property(e => e.CreateDate)
              .HasDefaultValueSql("GETDATE()")
              .ValueGeneratedOnAdd();

        // Настройка связи с OrderStatusEntity
        builder.HasOne(e => e.Status)
            .WithMany(s => s.Orders)
            .HasForeignKey(e => e.StatusId)
            .IsRequired(false);

        // Связь Many-to-Many с ProductsEntity
        builder.HasMany(e => e.Products)
            .WithMany(p => p.Orders)
            .UsingEntity(j => j.ToTable("OrderProducts"));

        // Связь с OrderDetailEntity
        builder.HasMany(e => e.Details)
            .WithOne(d => d.Order)
            .HasForeignKey(d => d.OrderId)
            .IsRequired();

        // Связь с InvoiceEntity
        builder.HasOne(e => e.Invoice)
            .WithOne()
            .HasForeignKey<InvoiceEntity>(e => e.OrderId)
            .IsRequired(false);

        builder.HasIndex(e => e.Id).IsUnique();
    }
}
