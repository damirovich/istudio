using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ISTUDIO.Infrastructure.AppDbContext.EntityConfiguration;

public class InvoiceDetailEntityConfiguration : IEntityTypeConfiguration<InvoiceDetailEntity>
{
    public void Configure(EntityTypeBuilder<InvoiceDetailEntity> builder)
    {
        builder.ToTable("InvoiceDetails");

        builder.HasKey(e => e.Id);

        builder.Property(e => e.ItemQuantity).IsRequired();
        builder.Property(e => e.ItemPrice).HasColumnType("decimal(18, 2)").IsRequired();

        builder.HasOne(e => e.Products)
            .WithMany()
            .HasForeignKey(e => e.ProductId)
            .IsRequired();

        builder.HasOne(e => e.Invoice)
            .WithMany(i => i.InvoiceDetails)
            .HasForeignKey(e => e.InvoiceId)
            .IsRequired();

        builder.HasIndex(e => e.Id).IsUnique();
    }
}