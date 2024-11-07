using ISTUDIO.Domain.EntityModel;

namespace ISTUDIO.Application.Features.FreedomPay.InitiPay.Commands.AddRequestInitPay;

public class CreateFreedomPayInitRequestCommand : IRequest<Result>, IMapWith<FreedomPayInitRequestEntity>
{
    public int PgOrderId { get; set; }
    public int PgMerchantId { get; set; }
    public decimal PgAmount { get; set; }
    public string PgDescription { get; set; }
    public string PgSalt { get; set; }
    public string PgSig { get; set; }
    public void Mapping(Profile profile)
    {
        profile.CreateMap<CreateFreedomPayInitRequestCommand, FreedomPayInitRequestEntity>()
            .ForMember(dest => dest.PgOrderId, opt => opt.MapFrom(src => src.PgOrderId))
            .ForMember(dest => dest.PgMerchantId, opt => opt.MapFrom(src => src.PgMerchantId))
            .ForMember(dest => dest.PgAmount, opt => opt.MapFrom(src => src.PgAmount))
            .ForMember(dest => dest.PgDescription, opt => opt.MapFrom(src => src.PgDescription))
            .ForMember(dest => dest.PgSalt, opt => opt.MapFrom(src => src.PgSalt))
            .ForMember(dest => dest.PgSig, opt => opt.MapFrom(src => src.PgSig));
    }
}
