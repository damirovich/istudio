using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ISTUDIO.Infrastructure.AppDbContext.EntityConfiguration;

public class StoryContentEntityConfiguration : IEntityTypeConfiguration<StoryContentEntity>
{
    public void Configure(EntityTypeBuilder<StoryContentEntity> builder)
    {
        builder.ToTable("StoryContents");

        builder.HasKey(e => e.Id);

        builder.Property(e => e.MediaUrl)
            .HasMaxLength(500)
            .IsRequired();

        builder.Property(e => e.Type)
            .HasMaxLength(50)
            .IsRequired();

        builder.Property(e => e.Queue)
            .IsRequired();

        // Связь с StoriesEntity (многие к одному)
        builder.HasOne(e => e.Story)
            .WithMany(s => s.StoryContents)
            .HasForeignKey(e => e.StoryId)
            .IsRequired();

        builder.HasIndex(e => new { e.StoryId, e.Queue }).IsUnique();
    }
}
