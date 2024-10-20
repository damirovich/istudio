using AutoMapper.QueryableExtensions;
using ISTUDIO.Application.Features.Products.DTOs;

namespace ISTUDIO.Application.Features.Products.Queries;
using ResModel = List<ProductsResponseDTO>;
public class GetWeeklyNewProductsQuery : IRequest<ResModel>
{
    public class Handler : IRequestHandler<GetWeeklyNewProductsQuery, ResModel>
    {
        private readonly IAppDbContext _appDbContext;
        private readonly IMapper _mapper;

        public Handler(IAppDbContext appDbContext, IMapper mapper)
        {
            _appDbContext = appDbContext;
            _mapper = mapper;
        }

        public async Task<ResModel> Handle(GetWeeklyNewProductsQuery query, CancellationToken cancellationToken)
        {
            var lastProduct = await _appDbContext.Products
                .OrderByDescending(p => p.CreateDate)
                .FirstOrDefaultAsync(cancellationToken);

            if (lastProduct == null)
            {
                return new ResModel();
            }

            var lastDate = lastProduct.CreateDate;
            var oneWeekAgo = lastDate.AddDays(-7);

            var products = await _appDbContext.Products
                .Include(d => d.Discount)
                .Include(m => m.Magazine)
                .AsNoTracking()
                .Where(p => p.CreateDate >= oneWeekAgo && p.IsActive == true)
                .ProjectTo<ProductsResponseDTO>(_mapper.ConfigurationProvider)
                .ToListAsync(cancellationToken);

            // Выполняем случайное упорядочивание на стороне клиента
            var random = new Random();
            var randomizedProducts = products.OrderBy(p => random.Next()).ToList();

            return randomizedProducts;
        }
    }
}
