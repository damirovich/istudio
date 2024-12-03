namespace ISTUDIO.Application.Features.BakaiPay.ConfirmTransaction;

using ISTUDIO.Domain.EntityModel;
using ResModel = Result;
public class ConfirmTranBakaiPayResCommand : IRequest<ResModel>, IMapWith<BakaiConfirmTranResEntity>
{
    public int CreateTranId { get; set; }
    public string Status { get; set; }
    public string OrderId { get; set; }

    public class Handler : IRequestHandler<ConfirmTranBakaiPayResCommand, ResModel>
    {
        private readonly IAppDbContext _appDbContext;
        private readonly IMapper _mapper;
        public Handler(IAppDbContext appDbContext, IMapper mapper) =>
           (_appDbContext, _mapper) = (appDbContext, mapper);

        public async Task<ResModel> Handle(ConfirmTranBakaiPayResCommand command, CancellationToken cancellationToken)
        {
            try
            {
                var confirmPayRes = _mapper.Map<BakaiConfirmTranResEntity>(command);

                await _appDbContext.BakaiConfirmTranResponse.AddAsync(confirmPayRes);
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
        profile.CreateMap<ConfirmTranBakaiPayResCommand, BakaiConfirmTranResEntity>()
            .ForMember(dest => dest.CreateTranId, opt => opt.MapFrom(src => src.CreateTranId))
            .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.Status))
            .ForMember(dest => dest.OrderId, opt => opt.MapFrom(src => src.OrderId));
    }
}

