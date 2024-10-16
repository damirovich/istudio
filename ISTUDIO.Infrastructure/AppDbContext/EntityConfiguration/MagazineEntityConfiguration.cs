using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ISTUDIO.Infrastructure.AppDbContext.EntityConfiguration;

public class MagazineEntityConfiguration : IEntityTypeConfiguration<MagazineEntity>
{
    public void Configure(EntityTypeBuilder<MagazineEntity> builder)
    {
        builder.ToTable("Magazines"); // Устанавливаем имя таблицы в базе данных

        builder.HasKey(e => e.Id); // Устанавливаем первичный ключ

        // Устанавливаем максимальную длину и обязательность для свойств Name, Description, Address, PhoneNumber, и PhotoLogoURL
        builder.Property(e => e.Name).HasMaxLength(250).IsRequired();
        builder.Property(e => e.Description).HasMaxLength(500);
        builder.Property(e => e.Address).HasMaxLength(500);
        builder.Property(e => e.PhoneNumber).HasMaxLength(20).IsRequired();
        builder.Property(e => e.PhotoLogoURL).HasMaxLength(500);
        builder.Property(e => e.UserId).IsRequired();

        // Связь с продуктами
        builder.HasMany(e => e.Products)
               .WithOne(p => p.Magazine)
               .HasForeignKey(p => p.MagazineId);

        builder.HasMany(e => e.OrderDetails)
               .WithOne(od => od.Magazine)
               .HasForeignKey(od => od.MagazineId);


        //// Связь с корзинами покупок
        //builder.HasMany(e => e.ShoppingCarts)
        //       .WithOne(s => s.Magazine)
        //       .HasForeignKey(s => s.MagazineId);

        // Устанавливаем уникальный индекс для свойства Name
        builder.HasIndex(e => e.Name).IsUnique();
    }
}
