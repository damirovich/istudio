using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ISTUDIO.Infrastructure.AppDbContext.EntityConfiguration;

public class OrderAddressConfiguration : IEntityTypeConfiguration<OrderAddressEntity>
{
    public void Configure(EntityTypeBuilder<OrderAddressEntity> builder)
    {
        builder.ToTable("OrderAddress");

        builder.HasKey(e => e.Id);

        builder.Property(e => e.Region).HasMaxLength(50).IsRequired();
        builder.Property(e => e.City).HasMaxLength(50);
        builder.Property(e => e.Address).HasMaxLength(500);
        builder.Property(e => e.Comments).HasMaxLength(500);
        builder.Property(e => e.UserId).IsRequired();


        builder.HasOne(e => e.Orders)
              .WithMany(c => c.OrderAddresses)
              .HasForeignKey(e => e.OrderId)
              .IsRequired(false);

        builder.HasIndex(e => e.Id).IsUnique();
    }
}
