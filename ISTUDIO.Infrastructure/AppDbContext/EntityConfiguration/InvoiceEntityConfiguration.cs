using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ISTUDIO.Infrastructure.AppDbContext.EntityConfiguration;

public class InvoiceEntityConfiguration : IEntityTypeConfiguration<InvoiceEntity>
{
    public void Configure(EntityTypeBuilder<InvoiceEntity> builder)
    {
        builder.ToTable("Invoices");

        builder.HasKey(e => e.Id);

        builder.Property(e => e.TotalValue).HasColumnType("decimal(18, 2)").IsRequired();
        builder.Property(e => e.DateIssued).IsRequired();

        builder.HasOne(e => e.Order)
            .WithOne(o => o.Invoice)
            .HasForeignKey<InvoiceEntity>(e => e.OrderId)
            .IsRequired(false);

        builder.HasMany(e => e.InvoiceDetails)
            .WithOne(d => d.Invoice)
            .HasForeignKey(d => d.InvoiceId)
            .IsRequired();

        builder.HasIndex(e => e.Id).IsUnique();
    }
}