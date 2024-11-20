using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ISTUDIO.Infrastructure.AppDbContext.EntityConfiguration;

public class FreedomPayInitReqEntityConfiguration : IEntityTypeConfiguration<FreedomPayInitReqEntity>
{
    public void Configure(EntityTypeBuilder<FreedomPayInitReqEntity> builder)
    {
        builder.ToTable("FreedomPayInitReq");

        builder.HasKey(e => e.Id);

        builder.Property(e => e.JsonData).IsRequired();

        builder.Property(e => e.CreatedDate)
         .IsRequired()
         .HasDefaultValueSql("GETDATE()"); // SQL Server функция для текущей даты
    }
}
