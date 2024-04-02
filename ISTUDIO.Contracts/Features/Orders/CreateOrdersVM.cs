using ISTUDIO.Application.Features.Orders.Commands.CreateOrders;
using ISTUDIO.Application.Features.Orders.DTOs;

namespace ISTUDIO.Contracts.Features.Orders;

public class CreateOrdersVM : IMapWith<CreateOrdersCommand>
{
    public string UserId { get; set; }
    public decimal TotalAmount { get; set; }
    public int TotalQuantyProduct { get; set; }
    public string ShippingAddress { get; set; }

    public ICollection<ProductOrderDTO> ProductOrders { get; set; } = new List<ProductOrderDTO>();

    public void Mapping(Profile profile)
    {
        profile.CreateMap<CreateOrdersVM, CreateOrdersCommand>();
    }
}
