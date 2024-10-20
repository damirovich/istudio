
namespace ISTUDIO.Application.Features.ShoppingCarts.Commands.EditShoppinCarts;

using System.ComponentModel.DataAnnotations;
using ResModel = Result;
public class ChangeQuantyProductCartCommand : IRequest<ResModel>
{
    [Required]
    public int CartId { get; set; }

    public int QuantyProduct { get; set; }

    public class Handler : IRequestHandler<ChangeQuantyProductCartCommand, ResModel>
    {
        private readonly IAppDbContext _appDbContext;

        public Handler(IAppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public async Task<ResModel> Handle(ChangeQuantyProductCartCommand command, CancellationToken cancellationToken)
        {

            var shoppingCart = await _appDbContext.ShoppingCarts
                .Include(sc => sc.Products)
                .FirstOrDefaultAsync(sc => sc.Id == command.CartId);

            if (shoppingCart == null)
                throw new NotFoundException("Корзина не найдена по Id");

            if (shoppingCart.Products == null || !shoppingCart.Products.Any())
                throw new NotFoundException("В корзине нет товаров.");

            var product = shoppingCart.Products.FirstOrDefault();
            if (product == null)
                throw new NotFoundException("Товар не найден в корзине.");
            
            if (product.QuantityInStock < command.QuantyProduct)
                throw new BadRequestException($"Недостаточно количество продукта. {product.Name} Доступный количество продуктов: {product.QuantityInStock}");

            shoppingCart.QuantyProduct = command.QuantyProduct;
            shoppingCart.IsDeleted = false;

            _appDbContext.ShoppingCarts.Update(shoppingCart);

            await _appDbContext.SaveChangesAsync(cancellationToken);

            return ResModel.Success();

        }
    }
}
