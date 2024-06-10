using ISTUDIO.Application.Features.ModelsDTO;
using ISTUDIO.Application.Features.Orders.Commands.CreateOrders;
using ISTUDIO.Application.Features.Orders.DTOs;

namespace ISTUDIO.Contracts.Features.Orders;

public class CreateOrdersVM : IMapWith<CreateOrdersCommand>
{
    public string UserId { get; set; }
    public decimal TotalAmount { get; set; }
    public int TotalQuantyProduct { get; set; }
    public string PaymentMethod { get; set; }
    public string? ReceiptPhotoBase64 { get; set; }
    public OrderAddressDTO OrderAddress { get; set; }
    public ICollection<ProductOrderDTO> ProductOrders { get; set; } = new List<ProductOrderDTO>();

    public void Mapping(Profile profile)
    {
        profile.CreateMap<CreateOrdersVM, CreateOrdersCommand>()
              .ForMember(dest => dest.ReceiptPhoto, opt => opt.MapFrom(src =>
                !string.IsNullOrEmpty(src.ReceiptPhotoBase64)
                ? Convert.FromBase64String(src.ReceiptPhotoBase64)
                : null));
    }
}
