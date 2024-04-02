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
                .Include(fp => fp.Products).ThenInclude(p => p.Images) // Включаем информацию о фотографиях продуктов
                .Include(fp => fp.Products).ThenInclude(p => p.Discount) // Включаем информацию о скидках продуктов
                .AsNoTracking()
                .Where(fp => fp.UserId == query.UserId)
                .OrderByDescending(fp => fp.Id)
                .ToListAsync();

            // Маппинг каждого элемента списка на FavoriteProductsResponseDTO
            var responseDto = favoriteProducts.Select(fp => _mapper.Map<ResModel>(fp)).ToList();

            return new ResModel
            {
                UserId = query.UserId,
                Products = responseDto.SelectMany(fp => fp.Products).ToList()
            };
        }
    }
}
