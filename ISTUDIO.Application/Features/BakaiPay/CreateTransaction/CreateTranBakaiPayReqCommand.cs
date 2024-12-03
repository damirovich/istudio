using ISTUDIO.Domain.EntityModel;

namespace ISTUDIO.Application.Features.BakaiPay.CreateTransaction;
using ResModel = Result;
public class CreateTranBakaiPayReqCommand : IRequest<ResModel>, IMapWith<BakaiCreateTranReqEntity>
{
    public string PaymentCode { get; set; }
    public string PhoneNumber { get; set; }
    public int Amount { get; set; }
    public string OrderId { get; set; }
    public class Handler : IRequestHandler<CreateTranBakaiPayReqCommand, ResModel>
    {
        private readonly IAppDbContext _appDbContext;
        private readonly IMapper _mapper;
        public Handler(IAppDbContext appDbContext, IMapper mapper) =>
           (_appDbContext, _mapper) = (appDbContext, mapper);

        public async Task<ResModel> Handle(CreateTranBakaiPayReqCommand command, CancellationToken cancellationToken)
        {
            try
            {
                var createPayReq = _mapper.Map<BakaiCreateTranReqEntity>(command);

                await _appDbContext.BakaiCreateTranRequest.AddAsync(createPayReq);
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
        profile.CreateMap<CreateTranBakaiPayReqCommand, BakaiCreateTranReqEntity>()
            .ForMember(dest => dest.PaymentCode, opt => opt.MapFrom(src => src.PaymentCode))
            .ForMember(dest => dest.PhoneNumber, opt => opt.MapFrom(src => src.PhoneNumber))
            .ForMember(dest => dest.Amount, opt => opt.MapFrom(src => src.Amount))
            .ForMember(dest => dest.OrderId, opt => opt.MapFrom(src => src.OrderId));
    }
}
