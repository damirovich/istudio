using AutoMapper.QueryableExtensions;
using ISTUDIO.Application.Features.Products.DTOs;

namespace ISTUDIO.Application.Features.Products.Queries;
using ResModel = PaginatedList<ProductsResponseDTO>;
public class GetProductsListQuery  : IRequest<ResModel>
{
    public PaginatedParameters Parameters { get; set; }

    public class Handler : IRequestHandler<GetProductsListQuery, ResModel>
    {
        private readonly IAppDbContext _appDbContext;
        private readonly IMapper _mapper;
        public Handler(IAppDbContext appDbContext, IMapper mapper)
        {
            _appDbContext = appDbContext;
            _mapper = mapper;
        }

        public async Task<ResModel> Handle(GetProductsListQuery query, CancellationToken cancellationToken)
        {
            try
            {
                var random = new Random();
                var products = await _appDbContext.Products
                    .Include(d => d.Discount)
                    .Include(m => m.Magazine)
                    .AsNoTracking()
                    .Where(p => p.IsActive == true)
                    .OrderByDescending(c => c.Id)
                    .ProjectTo<ProductsResponseDTO>(_mapper.ConfigurationProvider)
                    .ToListAsync(cancellationToken);

                // Выполняем случайное упорядочивание на стороне клиента
                var randomizedProducts = products.OrderBy(p => random.Next()).ToList();

                // Применяем пагинацию к уже случайно упорядоченному списку
                var paginatedList = randomizedProducts
                    .Skip((query.Parameters.PageNumber - 1) * query.Parameters.PageSize)
                    .Take(query.Parameters.PageSize)
                    .ToList();

                // Возвращаем новый PaginatedList
                return new ResModel(
                    paginatedList,
                    query.Parameters.PageNumber,
                    (int)Math.Ceiling(products.Count / (double)query.Parameters.PageSize),
                    products.Count
                );
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException(ex.Message, ex);
            }
        }
    }
}

