using ISTUDIO.Application.Features.Orders.DTOs;

namespace ISTUDIO.Application.Features.Orders.Commands.CreateOrders;

using ISTUDIO.Application.Features.ModelsDTO;


using ResModel = CreateOrderResponseDTO;
public class CreateOrdersCommand : IRequest<ResModel>
{
    public string UserId { get; set; }
    public decimal TotalAmount { get; set; }
    public int TotalQuantyProduct { get; set; }
    public string PaymentMethod { get; set; }
    public byte[] ReceiptPhoto { get; set; }
    public OrderAddressDTO OrderAddress { get; set; }
    public ICollection<ProductOrderDTO> ProductOrders { get; set; } = new List<ProductOrderDTO>();
}

