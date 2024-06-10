using AutoMapper.QueryableExtensions;
using ISTUDIO.Application.Features.Products.DTOs;
using System.ComponentModel.DataAnnotations;

namespace ISTUDIO.Application.Features.Products.Queries;
using ResModel = List<ProductsResponseDTO>;
public class GetProductsBySubCategoryId : IRequest<ResModel>
{
    [Required]
    public int CategoryId { get; set; }

    public class Handler : IRequestHandler<GetProductsBySubCategoryId, ResModel>
    {
        private readonly IAppDbContext _appDbContext;
        private readonly IMapper _mapper;
        public Handler(IAppDbContext appDbContext, IMapper mapper)
            => (_appDbContext, _mapper) = (appDbContext, mapper);

        public async Task<ResModel> Handle(GetProductsBySubCategoryId query, CancellationToken cancellationToken)
        {
            var products = _appDbContext.Products.Include(d => d.Discount)
              .AsNoTracking()
              .Where(c => c.CategoryId == query.CategoryId)
              .OrderByDescending(c => c.Id)
              .ProjectTo<ProductsResponseDTO>(_mapper.ConfigurationProvider)
              .ToListAsync(cancellationToken);

            return await products;
        }
    }
}

