namespace ISTUDIO.Application.Features.ShoppingCarts.Commands.CreateShoppingCarts;

using ISTUDIO.Domain.EntityModel;
using ResModel = Result;
public class AddProductToCartsCommand : IRequest<ResModel>
{
    public string UserId { get; set; }
    public int ProductId { get; set; }

    public class Handler : IRequestHandler<AddProductToCartsCommand, ResModel>
    {
        private readonly IAppDbContext _appDbContext;
        public Handler(IAppDbContext appDbContext) => _appDbContext = appDbContext;

        public async Task<ResModel> Handle(AddProductToCartsCommand command, CancellationToken cancellationToken)
        {

            var product = await _appDbContext.Products.FirstOrDefaultAsync(p => p.Id == command.ProductId);

            if (product == null)
            {
                throw new NotFoundException("Продукт не найден.");
            }
            if (product.QuantityInStock < 1)
            {
                throw new BadRequestException($"{product.Name} {product.Model}  в наличии не остался.");
            }

            var existingCarts = await _appDbContext.ShoppingCarts
                    .Include(cart => cart.Products)
                    .Where(cart => cart.UserId == command.UserId)
                    .ToListAsync();

            foreach (var cart in existingCarts)
            {
                var existingProductInCart = cart.Products.FirstOrDefault(p => p.Id == command.ProductId);
                if (existingProductInCart != null)
                {
                    if (product.QuantityInStock < cart.QuantyProduct + 1)
                    {
                        throw new BadRequestException($"{product.Name} {product.Model}  в наличии не остался.");
                    }
                    // Если продукт уже есть в текущей корзине, увеличиваем количество
                    cart.QuantyProduct++;
                    cart.CreateDate = DateTime.Now;
                    await _appDbContext.SaveChangesAsync(cancellationToken);
                    return ResModel.Success();
                }
            }

            var shoppingCart = new ShoppingCartEntity
            {
                UserId = command.UserId,
                Products = new List<ProductsEntity> { product },
                QuantyProduct = 1,
                CreateDate = DateTime.Now
            };

            _appDbContext.ShoppingCarts.Add(shoppingCart);
            await _appDbContext.SaveChangesAsync(cancellationToken);

            return ResModel.Success();

        }
    }
}
