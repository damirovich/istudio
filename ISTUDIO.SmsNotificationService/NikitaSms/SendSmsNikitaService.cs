using ISTUDIO.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace ISTUDIO.SmsNotificationService.NikitaSms;

// Класс SendSmsNikitaService, наследуемый от BackgroundService, выполняет задачи в фоновом режиме
public class SendSmsNikitaService : BackgroundService
{
    // Зависимости, которые будут внедрены через конструктор
    private readonly ISmsNikitaService _smsNikitaService;
    private readonly ILogger<SendSmsNikitaService> _logger;
    private readonly IServiceScopeFactory _serviceScopeFactory;

    // Конструктор, который принимает зависимости через внедрение зависимостей (Dependency Injection)
    public SendSmsNikitaService(ISmsNikitaService smsNikitaService,
                                ILogger<SendSmsNikitaService> logger,
                                IServiceScopeFactory serviceScopeFactory)
        => (_smsNikitaService, _logger, _serviceScopeFactory)
        = (smsNikitaService, logger, serviceScopeFactory);

    // Переопределение метода ExecuteAsync, который будет выполняться при запуске фоновой службы
    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        // Пока не получен сигнал отмены, сервис будет работать
        while (!stoppingToken.IsCancellationRequested)
        {
            try
            {
                // Создаем новый скоуп для получения экземпляра DbContext
                using (var scope = _serviceScopeFactory.CreateScope())
                {
                    // Получаем DbContext через скоуп
                    var dbContext = scope.ServiceProvider.GetRequiredService<IAppDbContext>();

                    // Получаем все запросы на отправку SMS, которые еще не были отправлены (StatusSendSMS = false)
                    var smsNikitaRequests = await dbContext.SmsNikitaRequests
                    .Where(s => !s.StatusSendSMS)
                    .ToListAsync(stoppingToken);

                    // Проходим по каждому запросу на отправку SMS
                    foreach (var request in smsNikitaRequests)
                    {
                        // Создаем модель запроса для отправки SMS
                        var smsRequest = new SmsNikitaRequestModel
                        {
                            Id = request.Id.ToString(), // Преобразуем ID в строку
                            Text = request.TextSms, // Текст SMS
                            Time = DateTime.Now.ToString("yyyyMMddHHmmss"), // Время в формате строки
                            Phones = request.PhonesNumber.Split(',') // Преобразуем строку номеров в массив
                        };

                        // Отправляем SMS через сервис ISmsNikitaService
                        var result = await _smsNikitaService.SendSms(smsRequest);

                        // Если статус ответа 0 (успешная отправка)
                        if (result.Status == 0)
                        {
                            // Обновляем статус запроса на отправленное (true)
                            request.StatusSendSMS = true;
                            dbContext.SmsNikitaRequests.Update(request);
                            await dbContext.SaveChangesAsync(stoppingToken); // Сохраняем изменения в базе данных
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                // Логируем исключение, если что-то пошло не так
                Console.WriteLine($"Произошла ошибка при отправке СМС: {ex.Message}");
            }

            // Задержка перед следующей итерацией, чтобы избежать высокой нагрузки на процессор
            await Task.Delay(TimeSpan.FromMinutes(1), stoppingToken);
        }
    }
}
