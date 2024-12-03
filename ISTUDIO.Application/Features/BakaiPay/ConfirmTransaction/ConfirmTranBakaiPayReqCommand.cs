using ISTUDIO.Domain.EntityModel;

namespace ISTUDIO.Application.Features.BakaiPay.ConfirmTransaction;
using ResModel = Result;
public class ConfirmTranBakaiPayReqCommand : IRequest<ResModel>, IMapWith<BakaiConfirmTranReqEntity>
{
    public int CreateTranId { get; set; }
    public string OTPCode { get; set; }
    public class Handler : IRequestHandler<ConfirmTranBakaiPayReqCommand, ResModel>
    {
        private readonly IAppDbContext _appDbContext;
        private readonly IMapper _mapper;
        public Handler(IAppDbContext appDbContext, IMapper mapper) =>
           (_appDbContext, _mapper) = (appDbContext, mapper);

        public async Task<ResModel> Handle(ConfirmTranBakaiPayReqCommand command, CancellationToken cancellationToken)
        {
            try
            {
                var confirmPayReq = _mapper.Map<BakaiConfirmTranReqEntity>(command);

                await _appDbContext.BakaiConfirmTranRequest.AddAsync(confirmPayReq);
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
        profile.CreateMap<ConfirmTranBakaiPayReqCommand, BakaiConfirmTranReqEntity>()
            .ForMember(dest => dest.CreateTranId, opt => opt.MapFrom(src => src.CreateTranId))
            .ForMember(dest => dest.OTPCode, opt => opt.MapFrom(src => src.OTPCode));
    }
}