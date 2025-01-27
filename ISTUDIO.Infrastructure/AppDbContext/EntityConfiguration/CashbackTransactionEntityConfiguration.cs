using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ISTUDIO.Infrastructure.AppDbContext.EntityConfiguration;

public class CashbackTransactionEntityConfiguration : IEntityTypeConfiguration<CashbackTransactionEntity>
{
    public void Configure(EntityTypeBuilder<CashbackTransactionEntity> builder)
    {
        builder.ToTable("CashbackTransactions");

        builder.HasKey(e => e.Id);
        builder.Property(e => e.UserId).HasMaxLength(250).IsRequired();
        builder.Property(e => e.Amount).HasColumnType("decimal(18,2)").IsRequired();
        builder.Property(e => e.TransactionType) .HasMaxLength(50).IsRequired();
        builder.Property(e => e.CreatedAt)
            .HasDefaultValueSql("GETDATE()")
            .ValueGeneratedOnAdd();

        builder.HasOne(e => e.Order)
            .WithMany(o => o.CashbackTransactions)
            .HasForeignKey(e => e.OrderId)
            .IsRequired(false);

        builder.HasIndex(e => e.Id).IsUnique();
    }
}
