using ISTUDIO.Domain.EntityModel;

namespace ISTUDIO.Application.Features.FreedomPay.InitiPay.Commands.AddResponseInitPay;

public class CreateFreedomPayInitResponseCommand : IRequest<Result>, IMapWith<FreedomPayInitResEntity>
{
    public string Status { get; set; }
    public string PaymentId { get; set; }
    public string RedirectUrl { get; set; }
    public string RedirectUrlType { get; set; }
    public string Salt { get; set; }
    public string Sig { get; set; }
    public string ResultUrl { get; set; }

    public void Mapping(Profile profile)
    {
        profile.CreateMap<CreateFreedomPayInitResponseCommand, FreedomPayInitResEntity>()
            .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.Status))
            .ForMember(dest => dest.PaymentId, opt => opt.MapFrom(src => src.PaymentId))
            .ForMember(dest => dest.RedirectUrl, opt => opt.MapFrom(src => src.RedirectUrl))
            .ForMember(dest => dest.RedirectUrlType, opt => opt.MapFrom(src => src.RedirectUrlType))
            .ForMember(dest => dest.Salt, opt => opt.MapFrom(src => src.Salt))
            .ForMember(dest => dest.Sig, opt => opt.MapFrom(src => src.Sig))
            .ForMember(dest => dest.ResultUrl, opt => opt.MapFrom(src => src.ResultUrl));
    }
}
