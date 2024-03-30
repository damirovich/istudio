
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
            try
            {
                var shoppingCart = await _appDbContext.ShoppingCarts
                    .FirstOrDefaultAsync(sc => sc.Id == command.CartId);

                if (shoppingCart == null)
                    return ResModel.Failure(new[] { "Shopping cart not found for the Id" });

                shoppingCart.QuantyProduct = command.QuantyProduct;

                _appDbContext.ShoppingCarts.Update(shoppingCart);

                await _appDbContext.SaveChangesAsync(cancellationToken);

                return ResModel.Success();
            }
            catch (Exception ex)
            {
                return ResModel.Failure(new[] { ex.Message ?? ex.InnerException.Message });
            }
        }
    }
}
    