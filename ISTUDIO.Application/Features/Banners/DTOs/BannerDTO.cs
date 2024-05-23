using ISTUDIO.Domain.EntityModel;

namespace ISTUDIO.Application.Features.Banners.DTOs;

public class BannerDTO : IMapWith<BannerEntity>
{
    public int Id { get; set; }
    public string PhotoUrl { get; set; }
    public int Status { get; set; }
    public int? CategoryId { get; set; }
    public int? DiscountId { get; set; }
    public int? ProductId { get; set; }

    public void Mapping(Profile profile)
    {
        profile.CreateMap<BannerEntity, BannerDTO>();
    }
}