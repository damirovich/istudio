using ISTUDIO.Infrastructure.Identity;
using Microsoft.AspNetCore.Identity;

namespace ISTUDIO.Infrastructure.AppDbContext;

public static class Roles
{
    public const string Admin = "Admin";
    public const string Manager = "Manager";
    public const string Users = "User";
    public const string MobileUser = "MobileUser";
}
public static class DataSeed
{
    private static ModelBuilder _modelBuilder;

    public static void DBInitializer(this ModelBuilder modelBuilder)
    {
        _modelBuilder = modelBuilder;
       // CreateAdmin();
        CreateRoles();
    }

    public static void CreateRoles()
    {
        var ROLE_ID = "7854d4909e2c42d69c91f0d1245e2dad";
        _modelBuilder.Entity<IdentityRole>().HasData(new IdentityRole
        {
            Id = ROLE_ID,
            Name = Roles.Users,
            NormalizedName = Roles.Users.ToUpper()
        });
    }
    private static void CreateAdmin()
    {
        var adminName = "Adminio";
        var adminEmail = "bashyrov@mail.ru";
        var adminPass = "123qwe";

        var ADMIN_ID = "3433d4a0-5175-4391-bd5f-4f7bf4a1fef3";

        var ROLE_ID = ADMIN_ID;

        _modelBuilder.Entity<IdentityRole>().HasData(new IdentityRole
        {
            Id = ROLE_ID,
            Name = Roles.Admin,
            NormalizedName = Roles.Admin.ToUpper()
        });

        var hasher = new PasswordHasher<AppUser>();
        _modelBuilder.Entity<AppUser>().HasData(new AppUser
        {
            Id = ADMIN_ID,
            UserName = adminName,
            NormalizedUserName = adminName.ToUpper(),
            Email = adminEmail,
            NormalizedEmail = adminEmail.ToUpper(),
            EmailConfirmed = true,
            PasswordHash = hasher.HashPassword(null, adminPass),
            SecurityStamp = string.Empty
        });

        _modelBuilder.Entity<IdentityUserRole<string>>().HasData(new IdentityUserRole<string>
        {
            RoleId = ROLE_ID,
            UserId = ADMIN_ID
        });
    }
}
