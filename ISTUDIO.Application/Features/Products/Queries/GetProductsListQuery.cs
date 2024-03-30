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
            var products = _appDbContext.Products.Include(d => d.Discount)
               .AsNoTracking()
               .OrderByDescending(c => c.Id)
               .ProjectTo<ProductsResponseDTO>(_mapper.ConfigurationProvider)
               .PaginatedListAsync(query.Parameters.PageNumber, query.Parameters.PageSize);

            return await products;
        }
    }
}
