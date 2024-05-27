
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
            try
            {
                var existingFavorite = await _appDbContext.FavoriteProducts
                        .Where(cart => cart.UserId == command.UserId)
                        .ToListAsync();

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
            catch (Exception ex)
            {
                return ResModel.Failure(new[] { ex.Message });
            }
        }
    }


}
