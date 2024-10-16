using AutoMapper.QueryableExtensions;
using ISTUDIO.Application.Features.Products.DTOs;
using System.ComponentModel.DataAnnotations;

namespace ISTUDIO.Application.Features.Products.Queries;
using ResModel = PaginatedList<ProductsResponseDTO>;
public class GetProductsByMagazineIdQuery : IRequest<ResModel>
{
    [Required]
    public int MagazineId { get; set; }
    [Required]
    public PaginatedParameters Parameters { get; set; }

    public class Handler : IRequestHandler<GetProductsByMagazineIdQuery, ResModel>
    {
        private readonly IAppDbContext _appDbContext;
        private readonly IMapper _mapper;
        public Handler(IAppDbContext appDbContext, IMapper mapper)
            => (_appDbContext, _mapper) = (appDbContext, mapper);

        public async Task<ResModel> Handle(GetProductsByMagazineIdQuery query, CancellationToken cancellationToken)
        {
            var products = _appDbContext.Products
              .Include(d => d.Discount)
              .Include(m => m.Magazine)
              .AsNoTracking()
              .Where(m => m.MagazineId == query.MagazineId)
              .OrderByDescending(c => c.Id)
              .ProjectTo<ProductsResponseDTO>(_mapper.ConfigurationProvider)
              .PaginatedListAsync(query.Parameters.PageNumber, query.Parameters.PageSize);

            return await products;
        }
    }
}
