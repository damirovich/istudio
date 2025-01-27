using ISTUDIO.Domain.EntityModel;

namespace ISTUDIO.Application.Features.PayMethodts.DTOs;

public class PayMethodDTO : IMapWith<PaymentMethodEntity>
{
    public int Id { get; set; }
    public string PayMethodName { get; set; }
    public string PaymentType { get; set; }

    public void Mapping(Profile profile)
    {
        profile.CreateMap<PaymentMethodEntity, PayMethodDTO>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
            .ForMember(dest => dest.PayMethodName, opt => opt.MapFrom(src => src.Name))
            .ForMember(dest => dest.PaymentType, opt => opt.MapFrom(src => src.PaymentType.Name));
    }

}
