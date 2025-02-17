using ISTUDIO.Application.Features.OrderPayments.Commands.UpdateStatusOrderPayment;
using ISTUDIO.Application.Features.Orders.Commands.EditOrders.UpdateStatusOrders;
using ISTUDIO.Application.Features.Orders.DTOs;
using ISTUDIO.Application.Features.Orders.Queries;
using ISTUDIO.BankPaymentStatusCheckerService.Interfaces;
using MediatR;
using Microsoft.Extensions.Logging;

namespace ISTUDIO.BankPaymentStatusCheckerService.Services;

public class OrderServices : IOrderServices
{
    private readonly IMediator _mediator;
    private readonly ILogger<OrderServices> _logger;

    public OrderServices(IMediator mediator, ILogger<OrderServices> logger)
    {
        _mediator = mediator;
        _logger = logger;
    }

    /// <summary>
    /// Получение списка заказов по статусу.
    /// </summary>
    public async Task<List<GetOrderWithStatusDTO>> GetOrdersWithStatusAsync(string statusOrder)
    {
        try
        {
            var query = new GetOrderWithStatusQuery { OrderStatus = statusOrder };
            return await _mediator.Send(query);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Ошибка при получении заказов со статусом {StatusOrder}", statusOrder);
            return new List<GetOrderWithStatusDTO>(); // Возвращаем пустой список при ошибке
        }
    }

    /// <summary>
    /// Обновление статуса заказа и платежа.
    /// </summary>
    public async Task<bool> UpdateStatusOrderPay(int orderId, string newStatusOrder)
    {
        try
        {
            // 1. Обновляем статус заказа
            var commandOrder = new UpdateStatusOrdersCommand { OrderId = orderId, OrderStatus = newStatusOrder };
            var resultOrder = await _mediator.Send(commandOrder);

            if (!resultOrder.Succeeded)
            {
                _logger.LogWarning("Не удалось обновить статус заказа ID {OrderId} на {Status}. Ошибка: {Error}", orderId, newStatusOrder, string.Join(", ", resultOrder.Errors));
                return false;
            }

            // 2. Обновляем статус платежа
            var commandOrderPay = new UpdateStatusOrderPayCommand { OrderId = orderId, Status = newStatusOrder };
            var resultOrderPay = await _mediator.Send(commandOrderPay);

            if (!resultOrderPay.Succeeded)
            {
                _logger.LogWarning("Статус заказа ID {OrderId} обновлен, но платеж не изменен. Ошибка: {Error}", orderId, string.Join(", ", resultOrderPay.Errors));
                return false;
            }

            _logger.LogInformation("Статус заказа и платежа для заказа ID {OrderId} успешно обновлены на {Status}.", orderId, newStatusOrder);
            return true;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Ошибка при обновлении статуса заказа ID {OrderId} на {Status}", orderId, newStatusOrder);
            return false;
        }
    }
}
