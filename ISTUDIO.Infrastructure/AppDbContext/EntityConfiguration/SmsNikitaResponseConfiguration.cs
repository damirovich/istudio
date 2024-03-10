using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ISTUDIO.Infrastructure.AppDbContext.EntityConfiguration;

public class SmsNikitaResponseConfiguration : IEntityTypeConfiguration<SmsNikitaResponse>
{
    public void Configure(EntityTypeBuilder<SmsNikitaResponse> builder)
    {
        builder.ToTable("SmsNikitaResponses");

        builder.HasKey(e => e.Id);

        builder.Property(e => e.Phones).IsRequired(false);
        builder.Property(e => e.SmsCount).IsRequired(false);
        builder.Property(e => e.Message).IsRequired(false);

        builder.HasOne(e => e.Request)
               .WithMany(r => r.Responses)
               .HasForeignKey(e => e.SmsRequestId)
               .IsRequired();

        builder.HasOne(e => e.SmsStatus)
               .WithMany(s => s.SmsNikitaResponses)
               .HasForeignKey(e => e.SmsStatusId)
               .IsRequired();
    }
}
