
using ISTUDIO.Domain.EntityModel;

namespace ISTUDIO.Application.Features.Discounts.DTOs;

public class DiscountResponseListDTO : IMapWith<DiscountEntity>
{
    public int Id { get; set; }
    public decimal PercenTage { get; set; }
    public DateTime StartTime { get; set; }
    public DateTime EndTime { get; set; }

    public void Mapping(Profile profile)
    {
        profile.CreateMap<DiscountEntity, DiscountResponseListDTO>();
    }
}
