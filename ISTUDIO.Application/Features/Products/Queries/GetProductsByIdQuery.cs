using AutoMapper.QueryableExtensions;
using ISTUDIO.Application.Features.Products.DTOs;
using System.ComponentModel.DataAnnotations;

namespace ISTUDIO.Application.Features.Products.Queries;
using ResModel = ProductsResponseDTO;
public class GetProductsByIdQuery : IRequest<ResModel>
{
    [Required]
    public int ProductId { get; set; }

    public class Handler : IRequestHandler<GetProductsByIdQuery, ResModel>
    {
        private readonly IAppDbContext _appDbContext;
        private readonly IMapper _mapper;
        public Handler(IAppDbContext appDbContext, IMapper mapper)
            => (_appDbContext, _mapper) = (appDbContext, mapper);

        public async Task<ResModel> Handle(GetProductsByIdQuery query, CancellationToken cancellationToken)
        {
            var products = await _appDbContext.Products
              .Include(d => d.Discount)
              .Include(m => m.Magazine)
              .AsNoTracking()
              .Where(c => c.Id == query.ProductId)
              .OrderByDescending(c => c.Id)
              .ProjectTo<ResModel>(_mapper.ConfigurationProvider)
              .FirstOrDefaultAsync();
             

            return products;
        }
    }
}


