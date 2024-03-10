using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ISTUDIO.Infrastructure.AppDbContext.EntityConfiguration;

public class CategoryEntityConfiguration : IEntityTypeConfiguration<CategoryEntity>
{
    public void Configure(EntityTypeBuilder<CategoryEntity> builder)
    {
        builder.ToTable("Categories"); // Устанавливаем имя таблицы в базе данных

        builder.HasKey(e => e.Id); // Устанавливаем первичный ключ

        // Устанавливаем максимальную длину для свойств Name, Description и ImageUrl
        builder.Property(e => e.Name).HasMaxLength(250).IsRequired();
        builder.Property(e => e.Description).HasMaxLength(500);
        builder.Property(e => e.ImageUrl).HasMaxLength(500);

        // Определение связи с самой собой (для иерархии подкатегорий)
        builder.HasMany(e => e.SubCategories)
               .WithOne(e => e.ParentCategory)
               .HasForeignKey(e => e.ParentCategoryId);

        // Устанавливаем уникальный индекс для свойства Name
        builder.HasIndex(e => e.Name).IsUnique();
    }
}