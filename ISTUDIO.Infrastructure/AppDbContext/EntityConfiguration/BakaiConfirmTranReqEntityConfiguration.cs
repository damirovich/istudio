

using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ISTUDIO.Infrastructure.AppDbContext.EntityConfiguration;

public class BakaiConfirmTranReqEntityConfiguration : IEntityTypeConfiguration<BakaiConfirmTranReqEntity>
{
    public void Configure(EntityTypeBuilder<BakaiConfirmTranReqEntity> builder)
    {
        builder.ToTable("BakaiConfirmTranReq");

        builder.HasKey(e => e.Id);

        builder.Property(e => e.OTPCode).HasMaxLength(50).IsRequired();
        builder.Property(e => e.CreateTranId).IsRequired();

        builder.HasIndex(e => e.Id).IsUnique();
    }
}
