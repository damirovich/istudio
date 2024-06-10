
using ISTUDIO.Application.Features.Banners.DTOs;

namespace ISTUDIO.Application.Features.Banners.Queries;
using ResModel = BannerDTO;
public class GetBannersByIdQuery : IRequest<ResModel>
{
    public int BannerId { get; set; }

    public class Handler : IRequestHandler<GetBannersByIdQuery, ResModel>
    {
        private readonly IAppDbContext _appDbContext;
        private readonly IMapper _mapper;
        private readonly IRedisCacheService _redisCacheService;

        public Handler(IAppDbContext appDbContext, IMapper mapper, IRedisCacheService redisCacheService)
        {
            _appDbContext = appDbContext;
            _mapper = mapper;
            _redisCacheService = redisCacheService;
        }

        public async Task<ResModel> Handle(GetBannersByIdQuery query, CancellationToken cancellationToken)
        {
            var banners = await _appDbContext.Banners
                .Include(p => p.Products)
                .Include(d => d.Discounts)
                .Include(c => c.Categories)
                .Where(s => s.Id == query.BannerId)
                .FirstOrDefaultAsync(cancellationToken);
            var bannersDto = _mapper.Map<ResModel>(banners);

            return bannersDto;
        }
    }
}
