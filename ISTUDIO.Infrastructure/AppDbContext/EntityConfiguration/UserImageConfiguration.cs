using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ISTUDIO.Infrastructure.AppDbContext.EntityConfiguration;

public class UserImageConfiguration : IEntityTypeConfiguration<UserImagesEntity>
{
    public void Configure(EntityTypeBuilder<UserImagesEntity> builder)
    {
        builder.ToTable("UserImages");

        builder.HasKey(e => e.Id);
        builder.Property(e => e.Id).ValueGeneratedOnAdd().UseIdentityColumn();

        builder.Property(e => e.TypeImg).HasMaxLength(250).IsRequired();
        builder.Property(e => e.Url).IsRequired();
        builder.Property(e => e.Name).HasMaxLength(350).IsRequired(false);
        builder.Property(e => e.ContentType).HasMaxLength(50).IsRequired();

        builder.HasIndex(e => e.Id).IsUnique();
    }
}
