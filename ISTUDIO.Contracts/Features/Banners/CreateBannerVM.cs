
using ISTUDIO.Application.Features.Banners.Commands.CreateBanners;

namespace ISTUDIO.Contracts.Features.Banners;

public class CreateBannerVM : IMapWith<CreateBannersCommand>
{
    [Required(ErrorMessage = "PhotoBanner is required.")]
    public string PhotoBannerBase64 { get; set; }

    [Required(ErrorMessage = "Status is required.")]
    public int Status { get; set; }

    public int? CategoryId { get; set; }
    public int? DiscountId { get; set; }
    public int? ProductId { get; set; }

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
