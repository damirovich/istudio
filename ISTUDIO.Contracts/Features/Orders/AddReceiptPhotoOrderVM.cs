using ISTUDIO.Application.Features.Orders.Commands.EditOrders.AddReceoptPhoto;

namespace ISTUDIO.Contracts.Features.Orders;

/// <summary>
/// Модель для добавления фото чека к заказу.
/// </summary>
public class AddReceiptPhotoOrderVM : IMapWith<AddReceipPhotoOrdersCommand>
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
    /// Конфигурация маппинга между AddReceiptPhotoOrderVM и AddReceipPhotoOrdersCommand.
    /// </summary>
    public void Mapping(Profile profile)
    {
        profile.CreateMap<AddReceiptPhotoOrderVM, AddReceipPhotoOrdersCommand>()
            .ForMember(dest => dest.OrdersId, opt => opt.MapFrom(src => src.OrdersId))
            .ForMember(dest => dest.ReceiptPhoto, opt => opt.MapFrom(src => Convert.FromBase64String(src.ReceiptPhotoBase64)));
    }
}
