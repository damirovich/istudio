using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ISTUDIO.Infrastructure.AppDbContext.EntityConfiguration;

public class SmsNikitaStatusConfiguration : IEntityTypeConfiguration<SmsNikitaStatus>
{
    public void Configure(EntityTypeBuilder<SmsNikitaStatus> builder)
    {
        builder.ToTable("SmsNikitaStatuses");

        builder.HasKey(e => e.Id);

        builder.Property(e => e.Name).HasMaxLength(300).IsRequired();
        builder.Property(e => e.Status).IsRequired();

        builder.HasMany(e => e.SmsNikitaResponses)
               .WithOne(r => r.SmsStatus)
               .HasForeignKey(r => r.SmsStatusId)
               .IsRequired();
    }
}