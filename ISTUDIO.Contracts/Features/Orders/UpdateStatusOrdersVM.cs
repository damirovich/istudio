using ISTUDIO.Application.Features.Orders.Commands.EditOrders.UpdateStatusOrders;

namespace ISTUDIO.Contracts.Features.Orders;

/// <summary>
/// Модель для обновления статуса заказа.
/// </summary>
public class UpdateStatusOrdersVM : IMapWith<UpdateStatusOrdersCommand>
{
    /// <summary>
    /// Уникальный идентификатор заказа.
    /// </summary>
    public int OrderId { get; set; }

    /// <summary>
    /// Новый статус заказа.
    /// </summary>
    public string OrderStatus { get; set; }

    /// <summary>
    /// Конфигурация маппинга между UpdateStatusOrdersVM и UpdateStatusOrdersCommand.
    /// </summary>
    public void Mapping(Profile profile)
    {
        profile.CreateMap<UpdateStatusOrdersVM, UpdateStatusOrdersCommand>();
    }
}