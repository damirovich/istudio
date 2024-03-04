using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ISTUDIO.Infrastructure.AppDbContext.EntityConfiguration;

public class FamilyUserConfiguration : IEntityTypeConfiguration<FamilyUserEntity>
{
    public void Configure(EntityTypeBuilder<FamilyUserEntity> builder)
    {
        builder.ToTable("FamilyUsers");

        builder.HasKey(e => e.Id);
        builder.Property(e => e.Id).ValueGeneratedOnAdd().UseIdentityColumn();

        builder.Property(e => e.FullName).HasMaxLength(250).IsRequired(false);
        builder.Property(e => e.PIN).HasMaxLength(50).IsRequired();
        builder.Property(e => e.PhoneNumber).HasMaxLength(50).IsRequired();
        builder.Property(e => e.PlaceOfWork).IsRequired(false);
        builder.Property(e => e.RelationDegreeClient).HasMaxLength(250).IsRequired(false);

        builder.HasIndex(e => e.Id).IsUnique();
    }
}
