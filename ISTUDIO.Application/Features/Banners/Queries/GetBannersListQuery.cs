using ISTUDIO.Application.Features.Banners.DTOs;

namespace ISTUDIO.Application.Features.Banners.Queries;
using ResModel = BannerListResponseDTO;
public class GetBannersListQuery : IRequest<ResModel>
{
    public class Handler : IRequestHandler<GetBannersListQuery, ResModel>
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

        public async Task<ResModel> Handle(GetBannersListQuery query, CancellationToken cancellationToken)
        {
            string cacheKey = "BannersIstudio";
            var cachedResult = await _redisCacheService.GetAsync<List<BannerDTO>>(cacheKey);

            if (cachedResult != null)
            {
                return new ResModel { Banners = cachedResult };
            }

            var banners = await _appDbContext.Banners
                .Include(s=>s.Products)
                .Include(s=>s.Discounts)
                .Include(s=>s.Categories)
                .ToListAsync(cancellationToken);
            var bannersDto = _mapper.Map<List<BannerDTO>>(banners);

            await _redisCacheService.SetAsync(cacheKey, bannersDto, TimeSpan.FromDays(10));
            return new ResModel { Banners = bannersDto };
        }
    }
}
