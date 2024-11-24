using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ISTUDIO.Infrastructure.AppDbContext.EntityConfiguration;

public class BakaiCreateTranResEntityConfiguration : IEntityTypeConfiguration<BakaiCreateTranResEntity>
{
    public void Configure(EntityTypeBuilder<BakaiCreateTranResEntity> builder)
    {
        builder.ToTable("BakaiCreateTranRes");

        builder.HasKey(e => e.Id);

        builder.Property(e => e.Status).HasMaxLength(250).IsRequired();
        builder.Property(e => e.OrderId).HasMaxLength(250).IsRequired(false);
        builder.Property(e => e.CreateId).IsRequired();
        builder.Property(e => e.PaymentCode).HasMaxLength(250).IsRequired();

        builder.HasIndex(e => e.Id).IsUnique();
    }
}
