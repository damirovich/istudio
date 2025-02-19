using ISTUDIO.Domain.EntityModel;

namespace ISTUDIO.Application.Features.Orders.DTOs;

public class OrderStatusDTO : IMapWith<OrderStatusEntity>
{
    public string NameRus { get; set; }
    public string NameEng { get; set; }

    public void Mapping(Profile profile)
    {
        profile.CreateMap<OrderStatusEntity, OrderStatusDTO>()
            .ForMember(dest => dest.NameRus, opt => opt.MapFrom(src => src.NameRus))
            .ForMember(dest => dest.NameEng, opt => opt.MapFrom(src => src.NameEng));
    }
}
