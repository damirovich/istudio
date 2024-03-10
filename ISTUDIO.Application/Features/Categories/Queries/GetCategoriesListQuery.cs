using ISTUDIO.Application.Features.Categories.DTOs;
using ISTUDIO.Application.Features.ModelsDTO;

namespace ISTUDIO.Application.Features.Categories.Queries;
using ResModel = CategoriesListResponseDTO;
public class GetCategoriesListQuery : IRequest<ResModel>
{
    public class Handler : IRequestHandler<GetCategoriesListQuery, ResModel>
    {
        private readonly IAppDbContext _appDbContext;
        private readonly IMapper _mapper;

        public Handler(IAppDbContext appDbContext, IMapper mapper)
        {
            _appDbContext = appDbContext;
            _mapper = mapper;
        }

        public async Task<ResModel> Handle(GetCategoriesListQuery query, CancellationToken cancellationToken)
        {
            var categories = await _appDbContext.Categories.ToListAsync(cancellationToken);

            // Преобразование категорий в CategoriesListResponseDTO

            var categoriesDto = _mapper.Map<List<CategoryDTO>>(categories);
            // Возврат результатов в виде PaginatedList
            return new ResModel { Categories = categoriesDto };
        }
    }
}
