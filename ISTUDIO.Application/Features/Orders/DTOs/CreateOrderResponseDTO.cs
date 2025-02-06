using ISTUDIO.Domain.EntityModel;

namespace ISTUDIO.Application.Features.Orders.DTOs;

public class CreateOrderResponseDTO
{
    public int OrderId { get; set; }

    public List<OrderPayMethodResDTO> PaymentMethods { get; set; } = new();
}

public class OrderPayMethodResDTO : IMapWith<PaymentMethodEntity>
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string PaymentType { get; set; }

    public void Mapping(Profile profile)
    {
        profile.CreateMap<PaymentMethodEntity, OrderPayMethodResDTO>()
                 //.ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                 //.ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
                 .ForMember(dest => dest.PaymentType, opt => opt.MapFrom(src => src.PaymentType != null ? src.PaymentType.Name : string.Empty));
    }
}