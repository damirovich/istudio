using AutoMapper.QueryableExtensions;
using ISTUDIO.Application.Features.CashbackProduct.DTOs;

namespace ISTUDIO.Application.Features.CashbackProduct.Queries;
using ResModel = PaginatedList<CashbackProductsDTO>;
public class GetCashbackProductsQuery : IRequest<ResModel>
{
    public PaginatedParameters Parameters { get; set; }

    public class Handler : IRequestHandler<GetCashbackProductsQuery, ResModel>
    {
        private readonly IAppDbContext _appDbContext;
        private readonly IMapper _mapper;

        public Handler(IAppDbContext appDbContext, IMapper mapper)
            => (_appDbContext, _mapper) = (appDbContext, mapper);

        public async Task<ResModel> Handle(GetCashbackProductsQuery request, CancellationToken cancellationToken)
        {
            var cashbackProducts = await _appDbContext.ProductCashbacks
               .Include(cp => cp.Product) // Включаем информацию о продукте
                   .ThenInclude(p => p.Images) // Включаем изображения продукта
               .OrderByDescending(cp => cp.Id)
               .ProjectTo<CashbackProductsDTO>(_mapper.ConfigurationProvider)
               .PaginatedListAsync(request.Parameters.PageNumber, request.Parameters.PageSize);

            return cashbackProducts;
        }
    }

}
