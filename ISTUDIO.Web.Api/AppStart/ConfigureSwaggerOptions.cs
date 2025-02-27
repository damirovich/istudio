using Asp.Versioning.ApiExplorer;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace ISTUDIO.Web.Api.AppStart;

public class ConfigureSwaggerOptions : IConfigureOptions<SwaggerGenOptions>
{
    private readonly IApiVersionDescriptionProvider _provider;

    public ConfigureSwaggerOptions(IApiVersionDescriptionProvider provider) => _provider = provider;

    public void Configure(SwaggerGenOptions options)
    {
        foreach (var description in _provider.ApiVersionDescriptions)
        {
            options.SwaggerDoc(description.GroupName, CreateInfoForApiVersion(description));
        }

        // Добавляем JWT авторизацию в Swagger
        options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
        {
            Description = "Введите 'Bearer' [пробел] и ваш JWT токен. Пример: 'Bearer your_token_here'",
            Name = "Authorization",
            In = ParameterLocation.Header,
            Type = SecuritySchemeType.ApiKey,
            Scheme = "Bearer"
        });

        options.AddSecurityRequirement(new OpenApiSecurityRequirement
        {
            {
                new OpenApiSecurityScheme
                {
                    Reference = new OpenApiReference
                    {
                        Type = ReferenceType.SecurityScheme,
                        Id = "Bearer"
                    },
                    Scheme = "oauth2",
                    Name = "Bearer",
                    In = ParameterLocation.Header,
                },
                new List<string>()
            }
        });

        // Добавляем CsmActionResult как стандартную модель ответа
        options.MapType<CsmActionResult>(() => new OpenApiSchema
        {
            Type = "object",
            Properties = new Dictionary<string, OpenApiSchema>
            {
                ["csmReturnStatuses"] = new OpenApiSchema { Type = "array", Items = new OpenApiSchema { Type = "object" } },
                ["data"] = new OpenApiSchema { Type = "object" }
            }
        });
    }

    /// <summary>
    /// Создает описание для каждой версии API
    /// </summary>
    private static OpenApiInfo CreateInfoForApiVersion(ApiVersionDescription description)
    {
        var info = new OpenApiInfo
        {
            Title = "My istudio API",
            Version = description.ApiVersion.ToString(),
            Description = $"ASP.NET Core Web API - Версия {description.ApiVersion}."
        };

        if (description.IsDeprecated)
        {
            info.Description += " ⚠️ Внимание: эта версия API устарела и будет удалена в будущих релизах.";
        }

        return info;
    }
}
