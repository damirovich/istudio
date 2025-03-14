﻿
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ISTUDIO.Infrastructure.AppDbContext.EntityConfiguration;

public class ProductsEntityConfiguration : IEntityTypeConfiguration<ProductsEntity>
{
    public void Configure(EntityTypeBuilder<ProductsEntity> builder)
    {
        builder.ToTable("Products");

        builder.HasKey(e => e.Id);

        builder.Property(e => e.Name).HasMaxLength(255).IsRequired();
        builder.Property(e => e.Model).HasMaxLength(100).IsRequired();
        builder.Property(e => e.Color).HasMaxLength(50).IsRequired();
        builder.Property(e => e.Price).HasColumnType("decimal(18, 2)").IsRequired();
        builder.Property(e => e.QuantityInStock).IsRequired();
        builder.Property(e => e.Description).IsRequired();
        builder.Property(e => e.CreateDate)
            .HasDefaultValue(DateTime.UtcNow)
            .ValueGeneratedOnAdd();
        builder.Property(e => e.IsActive).IsRequired();

        // Определение связи с категорией
        builder.HasOne(p => p.Category)
               .WithMany(c => c.Products)
               .HasForeignKey(p => p.CategoryId)
               .OnDelete(DeleteBehavior.Restrict);


        // Определяем внешний ключ для связи со скидкой
        builder.HasOne(p => p.Discount)
            .WithMany(d => d.Products)
            .HasForeignKey(p => p.DiscountId)
             .IsRequired(false)
            .OnDelete(DeleteBehavior.Restrict); // Устанавливаем ограничение на удаление каскадом

        builder.HasOne(p => p.Magazine)
           .WithMany(m => m.Products)
           .HasForeignKey(p => p.MagazineId)
           .IsRequired(false) // Если связь не обязательная
           .OnDelete(DeleteBehavior.Restrict); // Или другой подходящий тип удаления

        //// Определение связи с изображениями
        builder.HasMany(p => p.Images)
               .WithOne(pi => pi.Products)
               .HasForeignKey(pi => pi.ProductId)
                .IsRequired()
               .OnDelete(DeleteBehavior.Cascade);

        // Определение связи с заказами
        builder.HasMany(o => o.Orders)
             .WithMany(p => p.Products)
             .UsingEntity(j => j.ToTable("OrderProducts"));

        builder.HasMany(e => e.ShoppingCarts)
               .WithMany(p => p.Products)
               .UsingEntity(j => j.ToTable("ShoppingCartProducts"));

        // Добавляем связь с кешбэком
        builder.HasMany(p => p.ProductCashbacks)
               .WithOne(pc => pc.Product)
               .HasForeignKey(pc => pc.ProductId)
               .OnDelete(DeleteBehavior.Cascade); // Удаление кешбэков при удалении продукта


        // Связь с продуктом
        builder.HasOne(e => e.Cashback)
            .WithMany(p => p.Products)
            .HasForeignKey(e => e.CashbackId)
            .OnDelete(DeleteBehavior.Cascade);


        builder.HasIndex(e => e.Id).IsUnique();
    }
}