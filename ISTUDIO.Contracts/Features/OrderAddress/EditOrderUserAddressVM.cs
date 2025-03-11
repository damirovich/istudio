using ISTUDIO.Application.Features.OrderAddress.Commands.EditOrderAddress;

namespace ISTUDIO.Contracts.Features.OrderAddress;

/// <summary>
/// Модель для редактирования адреса пользователя при оформлении заказа.
/// </summary>
public class EditOrderUserAddressVM : IMapWith<EditOrderUserAddressCommand>
{
    /// <summary>
    /// Уникальный идентификатор адреса.
    /// </summary>
    [Required(ErrorMessage = "Id is required.")]
    public int Id { get; set; }

    /// <summary>
    /// Регион доставки.
    /// </summary>
    [Required(ErrorMessage = "Region is required.")]
    public string Region { get; set; }

    /// <summary>
    /// Город доставки.
    /// </summary>
    [Required(ErrorMessage = "City is required.")]
    public string City { get; set; }

    /// <summary>
    /// Точный адрес доставки (необязательно).
    /// </summary>
    public string? Address { get; set; }

    /// <summary>
    /// Дополнительные комментарии к заказу (необязательно).
    /// </summary>
    public string? Comments { get; set; }

    /// <summary>
    /// Идентификатор пользователя (необязательно).
    /// </summary>
    public string? UserId { get; set; }

    /// <summary>
    /// Конфигурация маппинга между EditOrderUserAddressVM и EditOrderUserAddressCommand.
    /// </summary>
    public void Mapping(Profile profile)
    {
        profile.CreateMap<EditOrderUserAddressVM, EditOrderUserAddressCommand>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
            .ForMember(dest => dest.Region, opt => opt.MapFrom(src => src.Region))
            .ForMember(dest => dest.City, opt => opt.MapFrom(src => src.City))
            .ForMember(dest => dest.Address, opt => opt.MapFrom(src => src.Address))
            .ForMember(dest => dest.Comments, opt => opt.MapFrom(src => src.Comments))
            .ForMember(dest => dest.UserId, opt => opt.MapFrom(src => src.UserId));
    }
}