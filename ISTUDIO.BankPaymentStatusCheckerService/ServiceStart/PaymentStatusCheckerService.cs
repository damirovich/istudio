using ISTUDIO.BankPaymentStatusCheckerService.Factory;
using ISTUDIO.BankPaymentStatusCheckerService.Interfaces;

namespace ISTUDIO.BankPaymentStatusCheckerService.ServiceStart;

public class PaymentStatusCheckerService : BackgroundService
{
    private readonly IServiceScopeFactory _serviceScopeFactory;
    private readonly ILogger<PaymentStatusCheckerService> _logger;
    private readonly TimeSpan _interval = TimeSpan.FromSeconds(180);

    public PaymentStatusCheckerService(IServiceScopeFactory serviceScopeFactory,
                                       ILogger<PaymentStatusCheckerService> logger)
    {
        _serviceScopeFactory = serviceScopeFactory;
        _logger = logger;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            try
            {
                using (var scope = _serviceScopeFactory.CreateScope())
                {
                    var factory = scope.ServiceProvider.GetRequiredService<BankPaymentCheckerFactory>();

                    // Выбираем банк
                    var checker = factory.CreateChecker("bakai"); // В будущем можно сделать динамическим
                    await checker.CheckAndUpdatePaymentsAsync();
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Ошибка при проверке статуса платежей.");
            }

            await Task.Delay(_interval, stoppingToken);
        }
    }
}
