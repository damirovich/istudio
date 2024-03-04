using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ISTUDIO.Infrastructure.AppDbContext.EntityConfiguration;

public class OrderDetailEntityConfiguration : IEntityTypeConfiguration<OrderDetailEntity>
{
    public void Configure(EntityTypeBuilder<OrderDetailEntity> builder)
    {
        builder.ToTable("OrderDetails");

        builder.HasKey(e => e.Id);

        builder.Property(e => e.UnitPrice).HasColumnType("decimal(18, 2)").IsRequired();
        builder.Property(e => e.Quantity).IsRequired();

        builder.HasOne(e => e.Order)
            .WithMany(o => o.Details)
            .HasForeignKey(e => e.OrderId)
            .IsRequired();

        builder.HasOne(e => e.Product)
            .WithMany()
            .HasForeignKey(e => e.ProductId)
            .IsRequired();

        builder.HasIndex(e => e.Id).IsUnique();
    }
}