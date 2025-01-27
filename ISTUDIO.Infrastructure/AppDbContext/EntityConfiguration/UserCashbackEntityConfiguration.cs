using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ISTUDIO.Infrastructure.AppDbContext.EntityConfiguration;

public class UserCashbackEntityConfiguration : IEntityTypeConfiguration<UserCashbackEntity>
{
    public void Configure(EntityTypeBuilder<UserCashbackEntity> builder)
    {
        builder.ToTable("UserCashbacks");

        builder.HasKey(e => e.Id);

        builder.Property(e => e.Amount).HasColumnType("decimal(18,2)").IsRequired();
        builder.Property(e => e.CreatedAt)
            .HasDefaultValueSql("GETDATE()")
            .ValueGeneratedOnAdd();

        builder.Property(e => e.ExpirationDate).IsRequired();
        builder.Property(e => e.Status).HasMaxLength(50).IsRequired();
        builder.Property(e => e.UserId).HasMaxLength(250).IsRequired();

        builder.HasIndex(e => e.Id).IsUnique();
    }
}
