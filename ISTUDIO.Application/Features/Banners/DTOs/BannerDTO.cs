using ISTUDIO.Domain.EntityModel;

namespace ISTUDIO.Application.Features.Banners.DTOs;

public class BannerDTO : IMapWith<BannerEntity>
{
    public int Id { get; set; }
    public string PhotoUrl { get; set; }
    public int Status { get; set; }
    public int? CategoryId { get; set; }
    public string? CategoryName { get; set; }
    public int? DiscountId { get; set; }
    public decimal? PercentAge { get; set; }
    public int? ProductId { get; set; }
    public string? ProductNameModel { get; set; }

    public void Mapping(Profile profile)
    {
        profile.CreateMap<BannerEntity, BannerDTO>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
            .ForMember(dest => dest.PhotoUrl, opt => opt.MapFrom(src => src.PhotoUrl))
            .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.Status))
            .ForMember(dest => dest.CategoryId, opt => opt.MapFrom(src => src.CategoryId))
            .ForMember(dest => dest.CategoryName, opt => opt.MapFrom(src => src.Categories.Name))
            .ForMember(dest => dest.DiscountId, opt => opt.MapFrom(src => src.DiscountId))
            .ForMember(dest => dest.PercentAge, opt => opt.MapFrom(src => src.Discounts.PercenTage))
            .ForMember(dest => dest.ProductId, opt => opt.MapFrom(src => src.ProductId))
            .ForMember(dest => dest.ProductNameModel, opt => opt.MapFrom(src => src.Products.Name + " " + src.Products.Model));
    }
}