using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ISTUDIO.Infrastructure.AppDbContext.EntityConfiguration;

public class BakaiCheckStatusResEntityConfiguration : IEntityTypeConfiguration<BakaiCheckStatusResEntity>
{
    public void Configure(EntityTypeBuilder<BakaiCheckStatusResEntity> builder)
{
    builder.ToTable("BakaiCheckStatusResponse");

    builder.HasKey(e => e.Id);

    builder.Property(e => e.PaymentCode).HasMaxLength(500).IsRequired();
    builder.Property(e => e.Status).HasMaxLength(250).IsRequired();
    builder.Property(e => e.OrderId).HasMaxLength(250).IsRequired(false);
    builder.Property(e => e.ConfirmedAt).HasMaxLength(250).IsRequired(false);
    builder.Property(e => e.ErrMsg).HasMaxLength(500).IsRequired(false);

    builder.HasIndex(e => e.Id).IsUnique();
}
}
