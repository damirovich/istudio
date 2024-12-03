using ISTUDIO.Web.Api.Mobile.AppStart;
using ISTUDIO.Application;
using ISTUDIO.Infrastructure;
using Microsoft.Extensions.Options;
using Serilog;
using Swashbuckle.AspNetCore.SwaggerGen;
using ISTUDIO.Web.Api.Mobile.Services.FreedomPayServices;
using ISTUDIO.Web.Api.Mobile.Services.BakaiPayService;
using ISTUDIO.Web.Api.Mobile.Services.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddApplication();
builder.Services.AddInfrastructure(builder.Configuration);
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddCustomAutoMapper();


//Json чтобы был как в моделях
builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.PropertyNamingPolicy = null;
    });
builder.Services.Configure<ApiClientsSettings>(builder.Configuration.GetSection("ApiClients"));

builder.Services.AddHttpClient<IFreedomPayApiClient, FreedomPayApiClient>((serviceProvider, client) =>
{
    var settings = serviceProvider.GetRequiredService<IOptions<ApiClientsSettings>>().Value;
    client.BaseAddress = new Uri(settings.FreedomPay.BaseAddress);
    client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
});

builder.Services.AddHttpClient<IBakaiPayApiClient, BakaiPayApiClient>((serviceProvider, client) =>
{
    var settings = serviceProvider.GetRequiredService<IOptions<ApiClientsSettings>>().Value;
    client.BaseAddress = new Uri(settings.BakaiPay.BaseAddress);
    client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
});
//Версионность в API
builder.Services.AddCustomApiVersioning();
//Swagger
builder.Services.AddTransient<IConfigureOptions<SwaggerGenOptions>, ConfigureSwaggerOptions>();
builder.Services.AddSwaggerGen(options =>
{
    options.OperationFilter<SwaggerDefaultValues>();
});
//Логиреование
var logger = new LoggerConfiguration()
    .ReadFrom.Configuration(builder.Configuration)
    .Enrich.FromLogContext()
    .CreateLogger();
builder.Logging.ClearProviders();
builder.Logging.AddSerilog(logger);


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
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
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
