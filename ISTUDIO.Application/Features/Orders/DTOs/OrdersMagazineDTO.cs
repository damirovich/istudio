
using ISTUDIO.Application.Features.ModelsDTO;
using ISTUDIO.Domain.EntityModel;

namespace ISTUDIO.Application.Features.Orders.DTOs;

public class OrdersMagazineDTO : IMapWith<OrderDetailEntity>
{
    public MagazineDTO Magazine { get; set; }
    public ProductOrderResDTO Product { get; set; }

    public void Mapping(Profile profile)
    {
        profile.CreateMap<OrderDetailEntity, OrdersMagazineDTO>()
            .ForMember(dest => dest.Magazine, opt => opt.MapFrom(src => src.Magazines))
            .ForMember(dest => dest.Product, opt => opt.MapFrom(src => src.Product));
    }
}
