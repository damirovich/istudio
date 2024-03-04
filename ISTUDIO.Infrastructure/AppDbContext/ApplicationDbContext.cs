
using ISTUDIO.Application.Common.Interfaces;
using ISTUDIO.Infrastructure.AppDbContext.EntityConfiguration;
using ISTUDIO.Infrastructure.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace ISTUDIO.Infrastructure.AppDbContext;

public class ApplicationDbContext : IdentityDbContext<AppUser>, IAppDbContext
{
    public ApplicationDbContext() { }
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options) { }
    public DbSet<FamilyUserEntity> FamilyUsers { get; set; }
    public DbSet<UserImagesEntity> UserImages { get; set; }
    public DbSet<CategoryEntity> Categories { get; set; }
    public DbSet<SubCategoryEntity> SubCategories { get; set; }
    public DbSet<DiscountEntity> Discounts { get; set; } 
    public DbSet<AppUser> AppUsers { get; set; }

    public Task<int> SaveChangesAsync() => base.SaveChangesAsync();
   
    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        builder.DBInitializer();

        builder.Entity<IdentityRole>(e => e.ToTable(name: "Roles"));
        builder.Entity<IdentityUserRole<string>>(e => e.ToTable(name: "UserRoles"));
        builder.Entity<IdentityUserClaim<string>>(e => e.ToTable(name: "UserClaim"));
        builder.Entity<IdentityUserLogin<string>>(e => e.ToTable("UserLogins"));
        builder.Entity<IdentityUserToken<string>>(e => e.ToTable("UserTokens"));
        builder.Entity<IdentityRoleClaim<string>>(e => e.ToTable("RoleClaims"));

        builder.ApplyConfiguration(new AppUserConfiguration());
        builder.ApplyConfiguration(new UserImageConfiguration());
        builder.ApplyConfiguration(new FamilyUserConfiguration());
        builder.ApplyConfiguration(new CategoryEntityConfiguration());
        builder.ApplyConfiguration(new SubCategoryEntityConfiguration());
        builder.ApplyConfiguration(new DiscountEntityConfiguration());
        builder.ApplyConfiguration(new InvoiceDetailEntityConfiguration());
        builder.ApplyConfiguration(new InvoiceDetailEntityConfiguration());
        builder.ApplyConfiguration(new OrderDetailEntityConfiguration());
        builder.ApplyConfiguration(new OrderEntityConfiguration());
        builder.ApplyConfiguration(new PaymentMethodEntityConfiguration());
        builder.ApplyConfiguration(new PaymentTypeEntityConfiguration());
        builder.ApplyConfiguration(new ProducImagesEntityConfiguration());
        builder.ApplyConfiguration(new ProductsEntityConfiguration());
        builder.ApplyConfiguration(new ShoppingCartEntityConfiguration());


    }
   
}
