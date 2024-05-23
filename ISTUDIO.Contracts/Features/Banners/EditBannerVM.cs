
using ISTUDIO.Application.Features.Banners.Commands.EditBanners;

namespace ISTUDIO.Contracts.Features.Banners;

public class EditBannerVM : IMapWith<EditBannerCommand>
{
    [Required(ErrorMessage = "BannerId is required.")]
    public int BannerId { get; set; }

    public string? PhotoBannerBase64 { get; set; }

    [Required(ErrorMessage = "Status is required.")]
    public int Status { get; set; }

    public int? CategoryId { get; set; }
    public int? DiscountId { get; set; }
    public int? ProductId { get; set; }

    public void Mapping(Profile profile)
    {
        profile.CreateMap<EditBannerVM, EditBannerCommand>()
            .ForMember(dest => dest.BannerId, opt => opt.MapFrom(src => src.BannerId))
            .ForMember(dest => dest.PhotoBanner, opt => opt.MapFrom(src => src.PhotoBannerBase64 != null ? Convert.FromBase64String(src.PhotoBannerBase64) : null))
            .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.Status))
            .ForMember(dest => dest.CategoryId, opt => opt.MapFrom(src => src.CategoryId))
            .ForMember(dest => dest.DiscountId, opt => opt.MapFrom(src => src.DiscountId))
            .ForMember(dest => dest.ProductId, opt => opt.MapFrom(src => src.ProductId));
    }
}
