
namespace ISTUDIO.Application.Features.FavoriteProducts.Commands.CreateFavoriteProducts;

using ISTUDIO.Domain.EntityModel;
using ResModel = Result;
public class CreateFavoriteProductsCommand : IRequest<ResModel>
{
    public string UserId { get; set; }
    public int ProductId { get; set; }

    public class Handler : IRequestHandler<CreateFavoriteProductsCommand, ResModel>
    {
        private readonly IAppDbContext _appDbContext;


        public Handler(IAppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public async Task<ResModel> Handle(CreateFavoriteProductsCommand command, CancellationToken cancellationToken)
        {
            // Проверка, существует ли уже избранный продукт для данного пользователя
            var existingFavorite = await _appDbContext.FavoriteProducts
                .Include(fp => fp.Products)
                .FirstOrDefaultAsync(fav => fav.UserId == command.UserId && fav.Products.Any(p => p.Id == command.ProductId), cancellationToken);

            if (existingFavorite != null)
            {
                throw new BadRequestException("Продукт уже добавлен в избранное.");
            }

            var product = await _appDbContext.Products.FirstOrDefaultAsync(p => p.Id == command.ProductId);

            if (product == null)
            {
                throw new NotFoundException("Продукт не найден.");
            }

            var favoriteProduct = new FavoriteProductsEntity
            {
                UserId = command.UserId,
                Products = new List<ProductsEntity> { product },
            };

            _appDbContext.FavoriteProducts.Add(favoriteProduct);
            await _appDbContext.SaveChangesAsync(cancellationToken);

            return ResModel.Success();

        }
    }


}
