using Asp.Versioning.ApiExplorer;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace ISTUDIO.Web.Api.FreedomPay.AppStart;

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
        options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
        {
            Description = $@"JWT Authorization header using the Bearer scheme.
                      Enter 'Bearer' [space] and then your token in the text input below.
                      Example: 'Bearer token'",
            Name = "Authorization",
            In = ParameterLocation.Header,
            Type = SecuritySchemeType.ApiKey,
            Scheme = "Bearer"
        });
        options.AddSecurityRequirement(new OpenApiSecurityRequirement()
            {
               {
                  new OpenApiSecurityScheme
                  {
                    Reference = new OpenApiReference
                       {
                          Type = ReferenceType.SecurityScheme,
                          Id = "Bearer"
                       },
                      Scheme = "oauth",
                      Name = "Bearer",
                      In = ParameterLocation.Header,

                    },
                    new List<string>()
               }
            });
    }
    private static OpenApiInfo CreateInfoForApiVersion(ApiVersionDescription description)
    {
        var info = new OpenApiInfo()
        {
            Title = "My FreedomPay istudio API",
            Version = description.ApiVersion.ToString(),
        };

        if (description.IsDeprecated)
            info.Description += " Deprecated.";
        return info;
    }
}
