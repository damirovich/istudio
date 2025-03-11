using ISTUDIO.Application.Features.OrderPayments.Commands.AddReceptPhotoPayment;

namespace ISTUDIO.Contracts.Features.OrderPayments;

/// <summary>
/// Модель для добавления фото чека к оплате заказа.
/// </summary>
public class AddReceiptPhotoOrderPaymentVM : IMapWith<AddReceipPhotoOrderPaymentCommand>
{
    /// <summary>
    /// Уникальный идентификатор заказа.
    /// </summary>
    [Required(ErrorMessage = "OrderId is required.")]
    public int OrdersId { get; set; }

    /// <summary>
    /// Фото чека в формате Base64.
    /// </summary>
    [Required(ErrorMessage = "ReceiptPhoto is required.")]
    public string ReceiptPhotoBase64 { get; set; }

    /// <summary>
    /// Конфигурация маппинга между AddReceiptPhotoOrderPaymentVM и AddReceipPhotoOrderPaymentCommand.
    /// </summary>
    public void Mapping(Profile profile)
    {
        profile.CreateMap<AddReceiptPhotoOrderPaymentVM, AddReceipPhotoOrderPaymentCommand>()
            .ForMember(dest => dest.OrdersId, opt => opt.MapFrom(src => src.OrdersId))
            .ForMember(dest => dest.ReceiptPhoto, opt => opt.MapFrom(src => Convert.FromBase64String(src.ReceiptPhotoBase64)));
    }
}