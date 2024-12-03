namespace ISTUDIO.Application.Features.BakaiPay.CreateTransaction;

using ISTUDIO.Domain.EntityModel;
using ResModel = Result;
public class CreateTranBakaiPayResCommand : IRequest<ResModel>, IMapWith<BakaiCreateTranResEntity>
{
    public string Status { get; set; }
    public string OrderId { get; set; }
    public int CreateId { get; set; }
    public string PaymentCode { get; set; }

    public class Handler : IRequestHandler<CreateTranBakaiPayResCommand, ResModel>
    {
        private readonly IAppDbContext _appDbContext;
        private readonly IMapper _mapper;
        public Handler(IAppDbContext appDbContext, IMapper mapper) =>
           (_appDbContext, _mapper) = (appDbContext, mapper);

        public async Task<ResModel> Handle(CreateTranBakaiPayResCommand command, CancellationToken cancellationToken)
        {
            try
            {
                var createTranRes =  _mapper.Map<BakaiCreateTranResEntity>(command);

                await _appDbContext.BakaiCreateTranResponse.AddAsync(createTranRes);
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
        profile.CreateMap<CreateTranBakaiPayResCommand, BakaiCreateTranResEntity>()
            .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.Status))
            .ForMember(dest => dest.OrderId, opt => opt.MapFrom(src => src.OrderId))
            .ForMember(dest => dest.CreateId, opt => opt.MapFrom(src => src.CreateId))
            .ForMember(dest => dest.PaymentCode, opt => opt.MapFrom(src => src.PaymentCode));
    }
}
