namespace ISTUDIO.Application.Features.OrderPayments.Commands.UpdateStatusOrderPayment;

using Microsoft.Extensions.Logging;
using ResModel = Result;

public class UpdateStatusOrderPayCommand : IRequest<ResModel>
{
    public int OrderId { get; set; }
    public string Status { get; set; }

    public class Handler : IRequestHandler<UpdateStatusOrderPayCommand, ResModel>
    {
        private readonly IAppDbContext _appDbContext;
        private readonly ILogger<Handler> _logger;

        public Handler(IAppDbContext appDbContext, ILogger<Handler> logger)
        {
            _appDbContext = appDbContext;
            _logger = logger;
        }

        public async Task<ResModel> Handle(UpdateStatusOrderPayCommand command, CancellationToken cancellationToken)
        {
            try
            {
                // Получаем статус заказа
                var status = await _appDbContext.OrderStatus
                    .FirstOrDefaultAsync(s => s.NameEng == command.Status, cancellationToken);

                if (status == null)
                {
                    _logger.LogWarning("Статус заказа '{Status}' не найден.", command.Status);
                    return ResModel.Failure(new[] { "OrderStatus не найден" });
                }

                // Получаем платеж заказа
                var existingOrderPay = await _appDbContext.OrderPayments
                    .FirstOrDefaultAsync(s => s.OrderId == command.OrderId, cancellationToken);

                if (existingOrderPay == null)
                {
                    _logger.LogWarning("Платеж для заказа ID {OrderId} не найден.", command.OrderId);
                    return ResModel.Failure(new[] { "OrderPayment не найден" });
                }

                // Обновляем статус платежа
                existingOrderPay.Status = status.NameEng;

                // Сохраняем изменения
                await _appDbContext.SaveChangesAsync(cancellationToken);

                _logger.LogInformation("Статус платежа для заказа ID {OrderId} обновлен на {Status}.", command.OrderId, command.Status);
                return ResModel.Success();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Ошибка при обновлении статуса платежа заказа ID {OrderId}", command.OrderId);
                return ResModel.Failure(new[] { $"Ошибка обновления статуса: {ex.Message}" });
            }
        }
    }
}
