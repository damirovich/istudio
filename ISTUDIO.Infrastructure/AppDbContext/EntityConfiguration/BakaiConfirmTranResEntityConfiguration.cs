using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ISTUDIO.Infrastructure.AppDbContext.EntityConfiguration;

public class BakaiConfirmTranResEntityConfiguration : IEntityTypeConfiguration<BakaiConfirmTranResEntity>
{
    public void Configure(EntityTypeBuilder<BakaiConfirmTranResEntity> builder)
    {
        builder.ToTable("BakaiConfirmTranRes");

        builder.HasKey(e => e.Id);

        builder.Property(e => e.Status).HasMaxLength(250).IsRequired();
        builder.Property(e => e.OrderId).HasMaxLength(250).IsRequired(false);
        builder.Property(e => e.CreateTranId).IsRequired();

        builder.HasIndex(e => e.Id).IsUnique();
    }
}