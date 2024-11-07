using ISTUDIO.Infrastructure.AppDbContext;
using ISTUDIO.Infrastructure.Identity;
using ISTUDIO.Infrastructure.Services;
using ISTUDIO.Infrastructure.Services.Integrations;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace ISTUDIO.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection
           services, IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("MsSQLConnectionString");
        services.AddTransient<IAppDbContext, ApplicationDbContext>();
        services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(connectionString, b => b.MigrationsAssembly("ISTUDIO.Infrastructure")));

        services
            .AddIdentity<AppUser, IdentityRole>()
            .AddEntityFrameworkStores<ApplicationDbContext>()
            .AddDefaultTokenProviders();

        services.Configure<IdentityOptions>(options =>
        {
            // Default Lockout settings.
            options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5);
            options.Lockout.MaxFailedAccessAttempts = 5;
            options.Lockout.AllowedForNewUsers = true;

            //Default Password settings.
            options.Password.RequireDigit = false;
            options.Password.RequireLowercase = true;
            options.Password.RequireNonAlphanumeric = false;//For special character
            options.Password.RequireUppercase = false;
            options.Password.RequiredLength = 6;
            options.Password.RequiredUniqueChars = 1;

            //Default SigIn settings.
            options.SignIn.RequireConfirmedEmail = false;
            options.SignIn.RequireConfirmedPhoneNumber = false;
            options.User.RequireUniqueEmail = true;

        });
        services.AddPersistence(configuration);

     
        return services;
    }

    public static IServiceCollection AddPersistence(
      this IServiceCollection services, IConfiguration configuration)
    {
        services.AddScoped<IIdentityService, IdentityService>();
        services.AddScoped<IAppUserService, AppUserServices>();
        services.AddScoped<IJwtUtils, JwtUtils>();
        services.AddScoped<ICurrentHttpRequest, CurrentHttpRequest>();
        services.AddHttpClient<ISmsNikitaService, SmsNikitaService>(client =>
        {
            client.BaseAddress = new Uri("https://smspro.nikita.kg/api/message");
        });
        services.AddHttpClient<IFreedomPayService, FreedomPayServices>(client =>
        {
            client.BaseAddress = new Uri(configuration["FreedomPay:BaseAddresFreedomPay"]);
        });
        services.AddSingleton<IRedisCacheService>(provider =>
        {
            var redisConnectionString = configuration.GetConnectionString("Redis");
            return new RedisCacheService(redisConnectionString);
        });
        var commonStoragePath = Path.Combine("..", "MyFiles", "shared_photos");
        services.AddSingleton<IFileStoreService>(provider =>
        {
            return new FileStoreService(commonStoragePath);
        });
        //services.AddSingleton<IFileStoreService>(provider =>
        //{
        //    var storagePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot");
        //    return new FileStoreService(storagePath);
        //});

        return services;
    }
}
