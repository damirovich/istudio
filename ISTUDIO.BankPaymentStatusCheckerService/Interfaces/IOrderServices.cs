using ISTUDIO.Application.Features.Orders.DTOs;

namespace ISTUDIO.BankPaymentStatusCheckerService.Interfaces;

public interface IOrderServices
{
    Task<List<GetOrderWithStatusDTO>> GetOrdersWithStatusAsync(string statusOrder);
    Task<bool> UpdateStatusOrderPay(int orderId, string newStatusOrder);
}
