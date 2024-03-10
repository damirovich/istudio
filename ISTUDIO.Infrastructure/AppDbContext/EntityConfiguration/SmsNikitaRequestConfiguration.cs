using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ISTUDIO.Infrastructure.AppDbContext.EntityConfiguration;

public class SmsNikitaRequestConfiguration : IEntityTypeConfiguration<SmsNikitaRequest>
{
    public void Configure(EntityTypeBuilder<SmsNikitaRequest> builder)
    {

        builder.ToTable("SmsNikitaRequests");

        builder.HasKey(e => e.Id);

        builder.Property(e => e.SenderCompany).HasMaxLength(255);
        builder.Property(e => e.TextSms).HasMaxLength(1000).IsRequired();
        builder.Property(e => e.Time).IsRequired();
        builder.Property(e => e.PhonesNumber).HasMaxLength(255).IsRequired();
        builder.Property(e => e.Test).IsRequired(false);

        // Определение связи с SmsNikitaResponse
        builder.HasMany(e => e.Responses)
               .WithOne(r => r.Request)
               .HasForeignKey(r => r.SmsRequestId)
               .IsRequired();
    }
}