using ISTUDIO.Application.Features.OrderPayments.Commands.AddReceptPhotoPayment;
using ISTUDIO.Application.Features.Orders.Commands.EditOrders.AddReceoptPhoto;
using ISTUDIO.Contracts.Features.Orders;

namespace ISTUDIO.Contracts.Features.OrderPayments;

public class AddReceiptPhotoOrderPaymentVM : IMapWith<AddReceipPhotoOrderPaymentCommand>
{
    [Required(ErrorMessage = "OrderId is required.")]
    public int OrdersId { get; set; }

    [Required(ErrorMessage = "ReceiptPhoto is required.")]
    public string ReceiptPhotoBase64 { get; set; }

    public void Mapping(Profile profile)
    {
        profile.CreateMap<AddReceiptPhotoOrderVM, AddReceipPhotoOrderPaymentCommand>()
            .ForMember(dest => dest.OrdersId, opt => opt.MapFrom(src => src.OrdersId))
            .ForMember(dest => dest.ReceiptPhoto, opt => opt.MapFrom(src => Convert.FromBase64String(src.ReceiptPhotoBase64)));
    }
}
