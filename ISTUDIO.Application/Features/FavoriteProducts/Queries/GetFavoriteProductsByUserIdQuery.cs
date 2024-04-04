using ISTUDIO.Application.Features.FavoriteProducts.DTOs;


namespace ISTUDIO.Application.Features.FavoriteProducts.Queries;
using ResModel = FavoriteProductsResponseDTO;
public class GetFavoriteProductsByUserIdQuery : IRequest<ResModel>
{
    public string UserId { get; set; }

    public class Handler : IRequestHandler<GetFavoriteProductsByUserIdQuery, ResModel>
    {
        private readonly IAppDbContext _appDbContext;
        private readonly IMapper _mapper;
        public Handler(IAppDbContext appDbContext, IMapper mapper)
            => (_appDbContext, _mapper) = (appDbContext, mapper);

        public async Task<ResModel> Handle (GetFavoriteProductsByUserIdQuery query, CancellationToken cancellationToken)
        {
            var favoriteProducts = await _appDbContext.FavoriteProducts
                .Include(fp => fp.Products).ThenInclude(p => p.Images)
                .Include(fp => fp.Products).ThenInclude(p => p.Discount)
                .AsNoTracking()
                .Where(fp => fp.UserId == query.UserId)
                .OrderByDescending(fp => fp.Id)
                .ProjectToListAsync<ResModel>(_mapper.ConfigurationProvider);

            return new ResModel
            {
                UserId = query.UserId,
                Products = favoriteProducts.SelectMany(fp => fp.Products).ToList()
            };
        }
    }
}
