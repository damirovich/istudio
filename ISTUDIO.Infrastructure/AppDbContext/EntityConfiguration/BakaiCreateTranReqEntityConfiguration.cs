using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ISTUDIO.Infrastructure.AppDbContext.EntityConfiguration;

public class BakaiCreateTranReqEntityConfiguration : IEntityTypeConfiguration<BakaiCreateTranReqEntity>
{
    public void Configure(EntityTypeBuilder<BakaiCreateTranReqEntity> builder)
    {
        builder.ToTable("BakaiCreateTranReq");

        builder.HasKey(e => e.Id);

        builder.Property(e => e.PaymentCode).HasMaxLength(100).IsRequired();
        builder.Property(e => e.PhoneNumber).HasMaxLength(15).IsRequired();
        builder.Property(e => e.Amount).IsRequired();
        builder.Property(e => e.OrderId).HasMaxLength(100).IsRequired(false);

        builder.HasIndex(e => e.Id).IsUnique();
    }
}
