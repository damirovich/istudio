using ISTUDIO.Application.Features.ModelsDTO;
using ISTUDIO.Application.Features.Orders.Commands.CreateOrders;
using ISTUDIO.Application.Features.Orders.DTOs;

namespace ISTUDIO.Contracts.Features.Orders;

/// <summary>
/// Модель для создания заказа.
/// </summary>
public class CreateOrdersVM : IMapWith<CreateOrdersCommand>
{
    /// <summary>
    /// Идентификатор пользователя, создающего заказ.
    /// </summary>
    public string UserId { get; set; }

    /// <summary>
    /// Общая сумма заказа.
    /// </summary>
    public decimal TotalAmount { get; set; }

    /// <summary>
    /// Общее количество товаров в заказе.
    /// </summary>
    public int TotalQuantyProduct { get; set; }

    /// <summary>
    /// Адрес доставки заказа.
    /// </summary>
    public OrderAddressDTO OrderAddress { get; set; }

    /// <summary>
    /// Список товаров, включенных в заказ.
    /// </summary>
    public ICollection<ProductOrderDTO> ProductOrders { get; set; } = new List<ProductOrderDTO>();

    /// <summary>
    /// Конфигурация маппинга между CreateOrdersVM и CreateOrdersCommand.
    /// </summary>
    public void Mapping(Profile profile)
    {
        profile.CreateMap<CreateOrdersVM, CreateOrdersCommand>();
    }
}