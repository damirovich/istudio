
namespace ISTUDIO.Application.Features.BakaiPay.CheckStatus;

using ISTUDIO.Domain.EntityModel;
using ResModel = Result;
public class CreateCheckStatusBakaiPayResCommand : IRequest<ResModel>, IMapWith<BakaiCheckStatusResEntity>
{
    public int CreateTranId { get; set; }
    public string PaymentCode { get; set; }
    public string Status { get; set; }
    public string OrderId { get; set; }
    public DateTime ConfirmedAt { get; set; }
    public string ErrMsg { get; set; }

    public class Handler : IRequestHandler<CreateCheckStatusBakaiPayResCommand, ResModel>
    {
        private readonly IAppDbContext _appDbContext;
        private readonly IMapper _mapper;
        public Handler(IAppDbContext appDbContext, IMapper mapper) =>
           (_appDbContext, _mapper) = (appDbContext, mapper);

        public async Task<ResModel> Handle(CreateCheckStatusBakaiPayResCommand command, CancellationToken cancellationToken)
        {
            try
            {
                var checkStatusRes = _mapper.Map<BakaiCheckStatusResEntity>(command); 

                await _appDbContext.BakaiCheckStatusResponse.AddAsync(checkStatusRes);
                await _appDbContext.SaveChangesAsync(cancellationToken);

                return ResModel.Success();
            }
            catch (Exception ex)
            {
                return ResModel.Failure(new[] { ex.InnerException?.Message ?? ex.Message });
            }
        }
    }

    public void Mapping(Profile profile)
    {
        profile.CreateMap<CreateCheckStatusBakaiPayResCommand, BakaiCheckStatusResEntity>()
            .ForMember(dest => dest.CreateTranId, opt => opt.MapFrom(src => src.CreateTranId))
            .ForMember(dest => dest.PaymentCode, opt => opt.MapFrom(src => src.PaymentCode))
            .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.Status))
            .ForMember(dest => dest.OrderId, opt => opt.MapFrom(src => src.OrderId))
            .ForMember(dest => dest.ConfirmedAt, opt => opt.MapFrom(src => src.ConfirmedAt))
            .ForMember(dest => dest.ErrMsg, opt => opt.MapFrom(src => src.ErrMsg));
    }
}
