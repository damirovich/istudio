using ISTUDIO.Application.Common.Interfaces;
using ISTUDIO.Infrastructure.API;
using ISTUDIO.Infrastructure.Identity;
using ISTUDIO.Web.UI.Features.Auth;
using Microsoft.AspNetCore.Components.Authorization;

namespace ISTUDIO.Web.UI.AppStart;

public static class DIExtensions
{
    public static void AddCustomDI(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddScoped<AuthenticationStateProvider, CustomAuthenticationStateProvider>();
        services.AddHttpClient<APIHttpClient>(x =>
        {
            x.BaseAddress = new Uri(@$"{configuration.GetSection("IstudioBackendAPI:Url").Value}");
        });
        services.AddHttpClient("CustomHttpClient", client =>
        {
            client.BaseAddress = new Uri(configuration.GetSection("IstudioBackendAPI:Url").Value);
        });
        services.AddScoped<IJwtUtils, JwtUtils>();
    }
}
