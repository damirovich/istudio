using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ISTUDIO.Infrastructure.AppDbContext.EntityConfiguration;
public class PaymentTypeEntityConfiguration : IEntityTypeConfiguration<PaymentTypeEntity>
{
    public void Configure(EntityTypeBuilder<PaymentTypeEntity> builder)
    {
        builder.ToTable("PaymentTypes");

        builder.HasKey(e => e.Id);

        builder.Property(e => e.Name).HasMaxLength(100).IsRequired();

        builder.HasMany(e => e.PaymentMethods)
            .WithOne(p => p.PaymentType)
            .HasForeignKey(p => p.PaymentTypeId)
            .IsRequired();

        builder.HasIndex(e => e.Id).IsUnique();
    }
}