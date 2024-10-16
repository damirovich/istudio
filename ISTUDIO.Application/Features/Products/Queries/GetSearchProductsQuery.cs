

using AutoMapper.QueryableExtensions;
using ISTUDIO.Application.Features.Products.DTOs;

namespace ISTUDIO.Application.Features.Products.Queries;
using ResModel = PaginatedList<ProductsResponseDTO>;
public class GetSearchProductsQuery : IRequest<ResModel>
{
    public PaginationWithSearchParameters Parameters { get; set; }

    public class Handler : IRequestHandler<GetSearchProductsQuery, ResModel>
    {
        private readonly IAppDbContext _appDbContext;
        private readonly IMapper _mapper;
        public Handler(IAppDbContext appDbContext, IMapper mapper)
        {
            _appDbContext = appDbContext;
            _mapper = mapper;
        }

        public async Task<ResModel> Handle(GetSearchProductsQuery query, CancellationToken cancellationToken)
        {
            var products = _appDbContext.Products
               .Include(d => d.Discount)
               .Include(m => m.Magazine)
               .AsNoTracking()
                  .Where(a => a.Name.Contains(query.Parameters.SearchTerm) ||
                         a.Id.ToString().Contains(query.Parameters.SearchTerm) ||
                         a.Model.Contains(query.Parameters.SearchTerm) ||
                         a.Description.StartsWith(query.Parameters.SearchTerm) 
                         )
               .OrderByDescending(c => c.Id)
               .ProjectTo<ProductsResponseDTO>(_mapper.ConfigurationProvider)
               .PaginatedListAsync(query.Parameters.PageNumber, query.Parameters.PageSize);

            return await products;
        }
    }
}
