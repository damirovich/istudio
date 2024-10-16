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
        builder.Property(e => e.Discount).HasColumnType("decimal(18, 2)").IsRequired(); // Новое поле для скидки
        //builder.Property(e => e.Subtotal).HasColumnType("decimal(18, 2)").IsRequired(); // Поле для общей суммы за товар
        //builder.Property(e => e.TotalPrice).HasColumnType("decimal(18, 2)").IsRequired(); // Поле для общей суммы с учетом скидки

        builder.HasOne(e => e.Order)
            .WithMany(o => o.Details)
            .HasForeignKey(e => e.OrderId)
            .IsRequired();

        builder.HasOne(e => e.Product)
            .WithMany()
            .HasForeignKey(e => e.ProductId)
            .IsRequired();

        builder.HasOne(e => e.Magazine)
            .WithMany(e=>e.OrderDetails)
            .HasForeignKey(e => e.MagazineId)
            .IsRequired(false);

        builder.Ignore(e => e.Subtotal); // Игнорируем вычисляемое свойство Subtotal
        builder.Ignore(e => e.TotalPrice); // Игнорируем вычисляемое свойство TotalPrice


        builder.HasIndex(e => e.Id).IsUnique();
    }
}