using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ISTUDIO.Infrastructure.AppDbContext.EntityConfiguration;

public class StoriesEntityConfiguration : IEntityTypeConfiguration<StoriesEntity>
{
    public void Configure(EntityTypeBuilder<StoriesEntity> builder)
    {
        builder.ToTable("Stories");

        builder.HasKey(e => e.Id);

        builder.Property(e => e.IconUrl)
            .HasMaxLength(500)
            .IsRequired();

        builder.Property(e => e.IsActive)
            .IsRequired();

        builder.Property(e => e.CreatedAt)
            .IsRequired();

        builder.Property(e => e.ExpireAt)
            .IsRequired();

        // Связь с StoryContentEntity (один ко многим)
        builder.HasMany(e => e.StoryContents)
            .WithOne(sc => sc.Story)
            .HasForeignKey(sc => sc.StoryId)
            .IsRequired();

        builder.HasIndex(e => e.CreatedAt);
    }
}
