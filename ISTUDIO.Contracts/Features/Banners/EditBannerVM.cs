
using ISTUDIO.Application.Features.Banners.Commands.EditBanners;
using ISTUDIO.Application.Features.Banners.DTOs;

namespace ISTUDIO.Contracts.Features.Banners;

/// <summary>
/// Модель запроса для редактирования баннера
/// </summary>
public class EditBannerVM : IMapWith<EditBannerCommand>
{
    /// <summary>
    /// Идентификатор баннера (обязательно)
    /// </summary>
    [Required(ErrorMessage = "Идентификатор баннера обязателен.")]
    public int BannerId { get; set; }

    /// <summary>
    /// Фото баннера в формате Base64 (необязательно)
    /// </summary>
    public string? PhotoBannerBase64 { get; set; }

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
    /// Настройка маппинга между EditBannerVM и EditBannerCommand
    /// </summary>
    /// <param name="profile">Профиль AutoMapper</param>
    public void Mapping(Profile profile)
    {
        profile.CreateMap<EditBannerVM, EditBannerCommand>()
            .ForMember(dest => dest.BannerId, opt => opt.MapFrom(src => src.BannerId))
            .ForMember(dest => dest.PhotoBanner, opt => opt.MapFrom(src => src.PhotoBannerBase64 != null ? Convert.FromBase64String(src.PhotoBannerBase64) : null))
            .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.Status))
            .ForMember(dest => dest.CategoryId, opt => opt.MapFrom(src => src.CategoryId))
            .ForMember(dest => dest.DiscountId, opt => opt.MapFrom(src => src.DiscountId))
            .ForMember(dest => dest.ProductId, opt => opt.MapFrom(src => src.ProductId));

        profile.CreateMap<BannerDTO, EditBannerVM>()
            .ForMember(dest => dest.BannerId, opt => opt.MapFrom(src => src.Id))
            .ForMember(dest => dest.PhotoBannerBase64, opt => opt.MapFrom(src => src.PhotoUrl))
            .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.Status))
            .ForMember(dest => dest.CategoryId, opt => opt.MapFrom(src => src.CategoryId))
            .ForMember(dest => dest.DiscountId, opt => opt.MapFrom(src => src.DiscountId))
            .ForMember(dest => dest.ProductId, opt => opt.MapFrom(src => src.ProductId));
    }
}