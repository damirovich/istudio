using ISTUDIO.Web.Api.Shop.AppStart;
using Microsoft.Extensions.Options;
using Serilog;
using ISTUDIO.Application;
using ISTUDIO.Infrastructure;
using Swashbuckle.AspNetCore.SwaggerGen;
using Azure;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.


builder.Services.AddControllers();
builder.Services.AddApplication();
builder.Services.AddInfrastructure(builder.Configuration);
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddCustomAutoMapper();


//Json чтобы был как в моделях
builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.PropertyNamingPolicy = null;
    });


//CORS
builder.Services.AddCors(c => c.AddPolicy("IstudioApiShop", opt =>
{
    opt.AllowAnyHeader(); // Разрешены любые заголовки.
    opt.AllowCredentials(); // Разрешены учетные данные (куки, авторизация).
    opt.AllowAnyMethod(); // Разрешены любые HTTP-методы (GET, POST, PUT и т.д.).
    opt.WithOrigins(builder.Configuration.GetSection("Cors:Urls").Get<string[]>()!); // Ограничение запросов только для заданных доменов.
}));

//Версионность в API
builder.Services.AddCustomApiVersioning();
//Swagger
builder.Services.AddTransient<IConfigureOptions<SwaggerGenOptions>, ConfigureSwaggerOptions>();
builder.Services.AddSwaggerGen(options =>
{
    // Подключение SwaggerDefaultValues как OperationFilter
    options.OperationFilter<SwaggerDefaultValues>();

    // Подключаем XML-документацию для текущего проекта
    var basePath = AppContext.BaseDirectory;
    var xmlPathMain = Path.Combine(basePath, $"{Assembly.GetExecutingAssembly().GetName().Name}.xml");
    options.IncludeXmlComments(xmlPathMain);

    // Подключаем XML-документацию для других проектов
    var xmlPathContracts = Path.Combine(basePath, "ISTUDIO.Contracts.xml");
    options.IncludeXmlComments(xmlPathContracts);

    options.SchemaFilter<EnumTypesSchemaFilter>(basePath);
    options.UseInlineDefinitionsForEnums(); // Если есть проблемы с enum
});
// Логирование
var logger = new LoggerConfiguration()
    .ReadFrom.Configuration(builder.Configuration)
    .Enrich.FromLogContext()
    .CreateLogger();
builder.Logging.ClearProviders();
builder.Logging.AddSerilog(logger);


var app = builder.Build();

app.UseStaticFiles();


app.UseSwagger();
app.UseSwaggerUI(options =>
{
    var descriptions = app.DescribeApiVersions();

    foreach (var description in descriptions)
    {
        var url = $"/swagger/{description.GroupName}/swagger.json";
        var name = description.GroupName;
        options.SwaggerEndpoint(url, name);
        options.DocExpansion(Swashbuckle.AspNetCore.SwaggerUI.DocExpansion.None);
    }
});


app.UseHttpsRedirection();

app.UseAuthorization();


app.UseCors("IstudioApiShop");

app.MapControllers();

app.Run();