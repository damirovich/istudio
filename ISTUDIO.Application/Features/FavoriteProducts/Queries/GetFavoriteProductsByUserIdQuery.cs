using ISTUDIO.Application.Features.FavoriteProducts.DTOs;
using ISTUDIO.Application.Features.Products.DTOs;


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
            var products = favoriteProducts.SelectMany(fp => fp.Products.Select(p => new ProductsFavoriteDTO
            {
                FavoriteProductId = fp.Id,
                Id = p.Id,
                Name = p.Name,
                Model = p.Model,
                Color = p.Color,
                Price = p.Price,
                QuantityInStock = p.QuantityInStock,
                Description = p.Description,
                Images = _mapper.Map<ICollection<ProductImagesDTO>>(p.Images),
                ProductDiscount = _mapper.Map<ProductDiscountDTO>(p.ProductDiscount),
                ProductCategory = p.ProductCategory
            })).ToList();
            return new ResModel
            {
                UserId = query.UserId,
                Products = products
            };
        }
    }
}
