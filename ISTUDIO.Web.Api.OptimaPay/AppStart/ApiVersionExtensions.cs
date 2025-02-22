using Asp.Versioning;

namespace ISTUDIO.Web.Api.OptimaPay.AppStart;

public static class ApiVersionExtensions
{
    // Метод-расширение для добавления настраиваемого версионирования API в IServiceCollection
    public static void AddCustomApiVersioning(this IServiceCollection services)
    {
        services.AddApiVersioning(options =>
        {
            // Установка версии API по умолчанию
            options.DefaultApiVersion = new ApiVersion(1.0);
            options.ReportApiVersions = true;
            options.ApiVersionReader = new UrlSegmentApiVersionReader();
        })
        .AddApiExplorer(options =>
        {
            options.SubstituteApiVersionInUrl = true;
            options.GroupNameFormat = "'v'VVV";
            options.AssumeDefaultVersionWhenUnspecified = true;
        });
    }
}
