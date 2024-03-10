using ISTUDIO.Infrastructure.Identity;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ISTUDIO.Infrastructure.AppDbContext.EntityConfiguration;

public class AppUserConfiguration : IEntityTypeConfiguration<AppUser>
{
    public void Configure(EntityTypeBuilder<AppUser> builder)
    {
        builder.ToTable("AppUsers");

        builder.HasKey(e => e.Id);

        builder.Property(e => e.FullName).HasMaxLength(250).IsRequired(false);
        builder.Property(e => e.PIN).HasMaxLength(50).IsRequired(false);
        builder.Property(e => e.SeriesDocument).HasMaxLength(20).IsRequired(false);
        builder.Property(e => e.NumDocument).HasMaxLength(20).IsRequired(false);
        builder.Property(e => e.Address).IsRequired(false);
        builder.Property(e => e.Email).HasMaxLength(200).IsRequired(false);
        builder.Property(e => e.NormalizedEmail).HasMaxLength(200).IsRequired(false);
        builder.Property(e => e.UserName).HasMaxLength(250).IsRequired();
        builder.Property(e => e.CodeOTP).IsRequired(false);
        builder.Property(e => e.CreatedDate)
           .HasDefaultValue(DateTime.UtcNow)
           .ValueGeneratedOnAdd();

        builder.HasMany(e => e.FamilyUsers)
            .WithOne()
            .HasForeignKey(f => f.UsersId)
            .IsRequired(false);

        builder.HasMany(e=>e.UserImages)
            .WithOne()
            .HasForeignKey(p=>p.UsersId)
            .IsRequired(false);

        builder.HasIndex(e => e.Id).IsUnique();
        builder.HasIndex(e => e.PIN).IsUnique();
    }
}
