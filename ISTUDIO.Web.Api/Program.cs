using ISTUDIO.Application;
using ISTUDIO.Infrastructure;
using ISTUDIO.Web.Api.AppStart;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Options;
using Serilog;
using Swashbuckle.AspNetCore.SwaggerGen;
using Asp.Versioning.ApiExplorer;
using System.Reflection;
using Swashbuckle.AspNetCore.SwaggerUI;


var builder = WebApplication.CreateBuilder(args);

// Логирование
var logger = new LoggerConfiguration()
    .ReadFrom.Configuration(builder.Configuration)
    .Enrich.FromLogContext()
    .CreateLogger();
builder.Logging.ClearProviders();
builder.Logging.AddSerilog(logger);

// Добавление сервисов
builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.PropertyNamingPolicy = null; // JSON, как в моделях
    });

builder.Services.AddApplication();
builder.Services.AddInfrastructure(builder.Configuration);
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddCustomAutoMapper();

// CORS
builder.Services.AddCors(c => c.AddPolicy("IstudioCustomAllow", opt =>
{
    opt.AllowAnyHeader();
    opt.AllowCredentials();
    opt.AllowAnyMethod();
    opt.WithOrigins(builder.Configuration.GetSection("Cors:Urls").Get<string[]>()!);
}));

// Версионность API
builder.Services.AddCustomApiVersioning();

// Swagger
builder.Services.AddTransient<IConfigureOptions<SwaggerGenOptions>, ConfigureSwaggerOptions>();
builder.Services.AddSwaggerGen(options =>
{
    options.OperationFilter<SwaggerDefaultValues>();

    var basePath = AppContext.BaseDirectory;
    var xmlPathMain = Path.Combine(basePath, $"{Assembly.GetExecutingAssembly().GetName().Name}.xml");
    var xmlPathContracts = Path.Combine(basePath, "ISTUDIO.Contracts.xml");

    // Подключаем XML-документацию только если файлы существуют
    SwaggerExtensions.IncludeXmlCommentsIfExists(options, xmlPathMain);
    SwaggerExtensions.IncludeXmlCommentsIfExists(options, xmlPathContracts);

    options.SchemaFilter<EnumTypesSchemaFilter>(basePath);
});


// Отключаем автоматическую обработку ошибок валидации
builder.Services.Configure<ApiBehaviorOptions>(options =>
{
    options.SuppressMapClientErrors = true;
});

// Аутентификация JWT
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(opt =>
    {
        opt.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = false,
            ValidateAudience = false,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = builder.Configuration["JwtOptions:Issuer"]!,
            ValidAudience = builder.Configuration["JwtOptions:Audience"]!,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["JwtOptions:Secret"]!))
        };
    });

builder.Services.AddAuthorization(opt => opt.DefaultPolicy =
    new AuthorizationPolicyBuilder(JwtBearerDefaults.AuthenticationScheme)
        .RequireAuthenticatedUser()
        .Build());

var app = builder.Build();

// Использование Swagger
app.UseSwagger();
app.UseSwaggerUI(options =>
{
    var provider = app.Services.GetRequiredService<IApiVersionDescriptionProvider>();

    foreach (var description in provider.ApiVersionDescriptions)
    {
        var url = $"/swagger/{description.GroupName}/swagger.json";
        var name = description.GroupName;

        if (description.IsDeprecated)
        {
            name += " (Deprecated)";
        }

        options.SwaggerEndpoint(url, name);
    }

    options.RoutePrefix = "swagger";
    options.DefaultModelsExpandDepth(0); // Полностью скрывает Models (Schemas)
    options.DefaultModelExpandDepth(0);   // Скрывает все эндпоинты
    options.DocExpansion(DocExpansion.None); // Запрещает авто-раскрытие устаревших API

    options.DisplayOperationId(); // Показывает ID, но не разворачивает
    options.DisplayRequestDuration(); // Показывает время выполнения запроса
});

// Middleware
app.UseMiddleware<TokenExpirationMiddleware>();
app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();
app.UseCors("IstudioCustomAllow");

// Контроллеры
app.MapControllers();

app.Run();
