using ISTUDIO.Application;
using ISTUDIO.Infrastructure;
using ISTUDIO.SmsNotificationService.ServiceStart;
using ISTUDIO.SmsNotificationService.NikitaSms;
using Serilog;

var builder = Host.CreateApplicationBuilder(args);
builder.Services.AddHostedService<SendSmsNikitaService>();

builder.Services.AddApplication();
builder.Services.AddInfrastructure(builder.Configuration);
builder.Services.AddCustomAutoMapper();

//Логиреование
var logger = new LoggerConfiguration()
    .ReadFrom.Configuration(builder.Configuration)
    .Enrich.FromLogContext()
    .CreateLogger();

builder.Logging.ClearProviders();
builder.Logging.AddSerilog(logger);

builder.Services.AddWindowsService(options =>
{
    options.ServiceName = "SmsNotificationService";
});

builder.Services.AddHostedService<SendSmsNikitaService>();


var host = builder.Build();
host.Run();
