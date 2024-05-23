using ISTUDIO.Application.Features.Categories.DTOs;

namespace ISTUDIO.Application.Features.Categories.Queries;
using ResModel = CategoriesListResponseDTO;
public class GetCategoriesListQuery : IRequest<ResModel>
{
    public class Handler : IRequestHandler<GetCategoriesListQuery, ResModel>
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

        public async Task<ResModel> Handle(GetCategoriesListQuery query, CancellationToken cancellationToken)
        {
            string cashKey = "CategoriesIstudio";
            var cashedResult = await _redisCacheService.GetAsync<List<CategoryDTO>>(cashKey);

            if (cashedResult != null)
            {
                return new ResModel { Categories = cashedResult };
            }
            var categories = await _appDbContext.Categories.ToListAsync(cancellationToken);
            var categoriesDto = _mapper.Map<List<CategoryDTO>>(categories);

            await _redisCacheService.SetAsync(cashKey, categoriesDto, TimeSpan.FromDays(10));
            // Возврат результатов в виде PaginatedList
            return new ResModel { Categories = categoriesDto };
        }
    }
}
