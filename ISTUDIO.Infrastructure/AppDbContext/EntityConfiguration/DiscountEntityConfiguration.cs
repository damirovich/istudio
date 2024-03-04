using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ISTUDIO.Infrastructure.AppDbContext.EntityConfiguration;

public class DiscountEntityConfiguration : IEntityTypeConfiguration<DiscountEntity>
{
    public void Configure(EntityTypeBuilder<DiscountEntity> builder)
    {
        builder.ToTable("Discounts");

        builder.HasKey(e => e.Id);

        builder.Property(e => e.PercenTage).HasColumnType("decimal(5, 2)").IsRequired();
        builder.Property(e => e.StartTime).IsRequired();
        builder.Property(e => e.EndTime).IsRequired();


        builder.HasOne(e => e.Products)
                  .WithOne(p => p.Discount)
                  .HasForeignKey<ProductsEntity>(p => p.Id)
                  .IsRequired();

        builder.HasIndex(e => e.Id).IsUnique();
    }
}
