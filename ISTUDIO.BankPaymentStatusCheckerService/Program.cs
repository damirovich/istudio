using ISTUDIO.BankPaymentStatusCheckerService.Banks.BakaiBank;
using ISTUDIO.BankPaymentStatusCheckerService.Factory;
using ISTUDIO.BankPaymentStatusCheckerService.Interfaces;
using ISTUDIO.BankPaymentStatusCheckerService.Services;
using ISTUDIO.BankPaymentStatusCheckerService.ServiceStart;

using ISTUDIO.Application;
using ISTUDIO.Infrastructure;
using ISTUDIO.Infrastructure.Services;
using Serilog;

var builder = Host.CreateApplicationBuilder(args);

builder.Services.AddApplication();
builder.Services.AddInfrastructure(builder.Configuration);
builder.Services.AddCustomAutoMapper();

builder.Services.AddHttpClient<IBakaiPaymentClient, BakaiPaymentClient>((serviceProvider, client) =>
{
    var configuration = serviceProvider.GetRequiredService<IConfiguration>();
    var baseAddress = configuration.GetValue<string>("ApiClients:BakaiPay:BaseAddress");

    if (string.IsNullOrEmpty(baseAddress))
    {
        throw new InvalidOperationException("BaseAddress для BakaiPay не найден в конфигурации.");
    }

    client.BaseAddress = new Uri(baseAddress);
    client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
});

//Логиреование
var logger = new LoggerConfiguration()
    .ReadFrom.Configuration(builder.Configuration)
    .Enrich.FromLogContext()
    .CreateLogger();

builder.Logging.ClearProviders();
builder.Logging.AddSerilog(logger);



builder.Services.AddWindowsService(options =>
{
    options.ServiceName = "BankPayStatusCheckServices";
});


builder.Services.AddScoped<IOrderServices, OrderServices>();
builder.Services.AddScoped<IAppUserService, AppUserServices>();

// Регистрируем обработчики платежей
builder.Services.AddScoped<BakaiPaymentStatusChecker>();
//builder.Services.AddScoped<OptimaPaymentStatusChecker>(); // В будущем можно добавить другие банки

// Регистрируем фабрику
builder.Services.AddSingleton<BankPaymentCheckerFactory>();

// Добавляем фоновый сервис
builder.Services.AddHostedService<PaymentStatusCheckerService>();

var host = builder.Build();
host.Run();
