
using ISTUDIO.Application.Features.Banners.Commands.CreateBanners;

namespace ISTUDIO.Contracts.Features.Banners;

/// <summary>
/// Модель запроса для создания баннера
/// </summary>
public class CreateBannerVM : IMapWith<CreateBannersCommand>
{
    /// <summary>
    /// Фото баннера в формате Base64 (обязательно)
    /// </summary>
    [Required(ErrorMessage = "Фото баннера обязательно.")]
    public string PhotoBannerBase64 { get; set; }

    /// <summary>
    /// Статус баннера (обязательно)
    /// </summary>
    [Required(ErrorMessage = "Статус обязателен.")]
    public int Status { get; set; }

    /// <summary>
    /// Идентификатор категории (необязательно)
    /// </summary>
    public int? CategoryId { get; set; }

    /// <summary>
    /// Идентификатор скидки (необязательно)
    /// </summary>
    public int? DiscountId { get; set; }

    /// <summary>
    /// Идентификатор продукта (необязательно)
    /// </summary>
    public int? ProductId { get; set; }

    /// <summary>
    /// Настройка маппинга между CreateBannerVM и CreateBannersCommand
    /// </summary>
    /// <param name="profile">Профиль AutoMapper</param>
    public void Mapping(Profile profile)
    {
        profile.CreateMap<CreateBannerVM, CreateBannersCommand>()
            .ForMember(dest => dest.PhotoBanner, opt => opt.MapFrom(src => Convert.FromBase64String(src.PhotoBannerBase64)))
            .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.Status))
            .ForMember(dest => dest.CategoryId, opt => opt.MapFrom(src => src.CategoryId))
            .ForMember(dest => dest.DiscountId, opt => opt.MapFrom(src => src.DiscountId))
            .ForMember(dest => dest.ProductId, opt => opt.MapFrom(src => src.ProductId));
    }
}
