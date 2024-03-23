using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ISTUDIO.Infrastructure.AppDbContext.EntityConfiguration;

public class CustomerImagesConfiguration : IEntityTypeConfiguration<CustomerImagesEntity>
{
    public void Configure(EntityTypeBuilder<CustomerImagesEntity> builder)
    {
        builder.ToTable("CustomerImages");

        builder.HasKey(e => e.Id);
        builder.Property(e => e.Id).ValueGeneratedOnAdd().UseIdentityColumn();

        builder.Property(e => e.TypeImg).HasMaxLength(250).IsRequired(false);
        builder.Property(e => e.Url).IsRequired();
        builder.Property(e => e.Name).HasMaxLength(350).IsRequired(false);
        builder.Property(e => e.UserId).IsRequired(false);


        builder.HasOne(e => e.Customers)
              .WithMany(c => c.CustomerImages)
              .HasForeignKey(e => e.CustomerId)
              .IsRequired(false);

        builder.HasIndex(e => e.Id).IsUnique();
    }
}
