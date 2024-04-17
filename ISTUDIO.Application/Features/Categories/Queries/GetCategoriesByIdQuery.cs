using ISTUDIO.Application.Features.Categories.DTOs;

namespace ISTUDIO.Application.Features.Categories.Queries;
using ResModel = CategoryResponseDTO;
public class GetCategoriesByIdQuery : IRequest<ResModel>
{
    public int Id { get; set; }

    public class Handler : IRequestHandler<GetCategoriesByIdQuery, ResModel>
    {
        private readonly IAppDbContext _appDbContext;
        private readonly IMapper _mapper;
        public Handler(IAppDbContext appDbContext, IMapper mapper)
        {
            _appDbContext = appDbContext;
            _mapper = mapper;
        }

        public async Task<ResModel> Handle(GetCategoriesByIdQuery query, CancellationToken cancellationToken)
        {
            var category = await _appDbContext.Categories
               .FirstOrDefaultAsync(c => c.Id == query.Id);

            if (category == null)
            {
                // Обработка ситуации, если категория не найдена
                throw new NotFoundException($"Категория с ID {query.Id} не найдена.");
            }

            // Маппинг объекта Category на объект CategoryDTO
            var categoryDTO = _mapper.Map<ResModel>(category);

            return categoryDTO;
        }
    }
}
