namespace ISTUDIO.Application.Features.Orders.Commands.CreateOrders;

using ISTUDIO.Application.Features.Orders.DTOs;

using ResModel = Result;
public class CreateOrdersCommand : IRequest<ResModel>
{
    public string UserId { get; set; }
    public decimal TotalAmount { get; set; }
    public int TotalQuantyProduct { get; set; }
    public string ShippingAddress { get; set; }
    public ICollection<ProductOrderDTO> ProductOrders { get; set; } = new List<ProductOrderDTO>();
}
