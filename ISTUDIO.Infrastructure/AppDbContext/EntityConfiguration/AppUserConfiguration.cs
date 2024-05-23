using ISTUDIO.Infrastructure.Identity;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ISTUDIO.Infrastructure.AppDbContext.EntityConfiguration;

public class AppUserConfiguration : IEntityTypeConfiguration<AppUser>
{
    public void Configure(EntityTypeBuilder<AppUser> builder)
    {
        builder.ToTable("AppUsers");

        builder.HasKey(e => e.Id);

        builder.Property(e => e.Email).HasMaxLength(200).IsRequired(false);
        builder.Property(e => e.NormalizedEmail).HasMaxLength(200).IsRequired(false);
        builder.Property(e => e.UserName).HasMaxLength(250).IsRequired();
        builder.Property(e => e.CodeOTP).IsRequired(false);
        builder.Property(e => e.CreatedDate)
           .HasDefaultValue(DateTime.UtcNow)
           .ValueGeneratedOnAdd();

        builder.Property(e => e.PhotoUsersUrl).HasMaxLength(500).IsRequired(false);

        builder.HasIndex(e => e.Id).IsUnique();
        
    }
}
