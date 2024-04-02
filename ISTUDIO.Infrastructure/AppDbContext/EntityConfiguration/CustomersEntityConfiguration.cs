using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ISTUDIO.Infrastructure.AppDbContext.EntityConfiguration;

public class CustomersEntityConfiguration : IEntityTypeConfiguration<CustomersEntity>
{
    public void Configure(EntityTypeBuilder<CustomersEntity> builder)
    {
        builder.ToTable("Customers");

        builder.HasKey(e => e.Id);

        builder.Property(e => e.PIN).HasMaxLength(50).IsRequired();
        builder.Property(e => e.FullName).HasMaxLength(250);
        builder.Property(e => e.Name).HasMaxLength(100);
        builder.Property(e => e.Surname).HasMaxLength(100);
        builder.Property(e => e.Patronymic).HasMaxLength(100);
        builder.Property(e => e.Sex).HasMaxLength(10);
        builder.Property(e => e.Nationality).HasMaxLength(100);
        builder.Property(e => e.SeriesNumDocument).HasMaxLength(50);
        builder.Property(e => e.PlaceOfBirth).HasMaxLength(250);
        builder.Property(e => e.Authority).HasMaxLength(250);
        builder.Property(e => e.Ethnicity).HasMaxLength(100);
        builder.Property(e => e.Address).HasMaxLength(500);
        builder.Property(e => e.Identification).IsRequired(false);


        builder.HasMany(e => e.CustomerImages)
               .WithOne(c => c.Customers)
               .HasForeignKey(c => c.CustomerId)
               .IsRequired();

        builder.HasMany(o => o.Orders)
            .WithMany(p => p.Customers)
            .UsingEntity(j => j.ToTable("CustomerOrders"));

        builder.HasIndex(e => e.Id).IsUnique();
        builder.HasIndex(e => e.PIN).IsUnique();
    }
}
