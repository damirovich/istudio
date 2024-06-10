using ISTUDIO.Application.Features.Orders.Commands.EditOrders.AddReceoptPhoto;

namespace ISTUDIO.Contracts.Features.Orders;

public class AddReceiptPhotoOrderVM : IMapWith<AddReceipPhotoOrdersCommand>
{
    [Required(ErrorMessage = "OrderId is required.")]
    public int OrdersId { get; set; }

    [Required(ErrorMessage = "ReceiptPhoto is required.")]
    public string ReceiptPhotoBase64 { get; set; }

    public void Mapping(Profile profile)
    {
        profile.CreateMap<AddReceiptPhotoOrderVM, AddReceipPhotoOrdersCommand>()
            .ForMember(dest => dest.OrdersId, opt => opt.MapFrom(src => src.OrdersId))
            .ForMember(dest => dest.ReceiptPhoto, opt => opt.MapFrom(src => Convert.FromBase64String(src.ReceiptPhotoBase64)));
    }
}
