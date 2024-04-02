using ISTUDIO.Domain.EntityModel;

namespace ISTUDIO.Application.Features.Products.DTOs;

public class ProductDiscountDTO  : IMapWith<DiscountEntity>
{
    public decimal PercenTage { get; set; }
    public DateTime StartTime { get; set; }
    public DateTime EndTime { get; set; }

    public void Mapping(Profile profile)
    {
        profile.CreateMap<DiscountEntity,ProductDiscountDTO>()
            .ReverseMap();
    }
}
