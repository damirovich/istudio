using ISTUDIO.Domain.EntityModel;

namespace ISTUDIO.Application.Features.Orders.DTOs;

public class GetOrderWithStatusDTO : IMapWith<OrderEntity>
{
    public int OrderId { get; set; }
    public string OrderStatus { get; set; }
    public string OrderPayStatus { get; set; }
    public int CreateTranId { get; set; }
    public string PaymentMethod { get; set; }

    public void Mapping(Profile profile)
    {
        profile.CreateMap<OrderEntity, GetOrderWithStatusDTO>()
            .ForMember(dest => dest.OrderId, opt => opt.MapFrom(src => src.Id))
            .ForMember(dest => dest.OrderStatus, opt => opt.MapFrom(src => src.Status.NameEng))
            .ForMember(dest => dest.OrderPayStatus, opt => opt.MapFrom(src => src.Payments.FirstOrDefault().Status))
            //.ForMember(dest => dest.CreateTranId, opt => opt.MapFrom(src => src.BakaiConfirmTranRes.FirstOrDefault().CreateTranId))
            .ForMember(dest => dest.PaymentMethod, opt => opt.MapFrom(src => src.Payments.FirstOrDefault().PaymentMethod.Name));
    }
}
