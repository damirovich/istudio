using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ISTUDIO.Infrastructure.AppDbContext.EntityConfiguration;

public class OrderStatusEntityConfiguration : IEntityTypeConfiguration<OrderStatusEntity>
{
    public void Configure(EntityTypeBuilder<OrderStatusEntity> builder)
    {
        builder.ToTable("OrderStatusEntity");

        builder.HasKey(e => e.Id);

        builder.Property(e => e.NameRus)
            .HasMaxLength(250)
            .IsRequired();

        builder.Property(e => e.NameEng)
          .HasMaxLength(250)
          .IsRequired();

        builder.Property(e => e.Description)
            .HasMaxLength(255)
            .IsRequired(false);

        // Связь с OrderEntity
        builder.HasMany(e => e.Orders)
            .WithOne(o => o.Status)
            .HasForeignKey(o => o.StatusId)
            .IsRequired();

        builder.HasIndex(e => e.NameEng).IsUnique();
    }
}
