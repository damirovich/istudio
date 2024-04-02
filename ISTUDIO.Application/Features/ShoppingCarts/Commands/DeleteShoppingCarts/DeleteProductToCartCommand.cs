namespace ISTUDIO.Application.Features.ShoppingCarts.Commands.DeleteShoppingCarts;

using System.ComponentModel.DataAnnotations;
using ResModel = Result;
public class DeleteProductToCartCommand : IRequest<ResModel>
{
    [Required]
    public int CartId { get; set; }

    public class Handler : IRequestHandler<DeleteProductToCartCommand, ResModel>
    {
        private readonly IAppDbContext _appDbContext;

        public Handler(IAppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public async Task<ResModel> Handle(DeleteProductToCartCommand command, CancellationToken cancellationToken)
        {
            try
            {
                var shoppingCart = await _appDbContext.ShoppingCarts
                    .FirstOrDefaultAsync(sc => sc.Id == command.CartId);

                if (shoppingCart == null)
                    return ResModel.Failure(new[] { "Shopping cart not found for the Id" });


                _appDbContext.ShoppingCarts.Remove(shoppingCart);

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


public class DeleteProductToCartCommandValidator : AbstractValidator<DeleteProductToCartCommand>
{
    public DeleteProductToCartCommandValidator()
    {
        RuleFor(v => v.CartId).NotEmpty().WithMessage("CartId не должен быть пустым.")
            .GreaterThan(0).WithMessage("CartId должен быть положительным числом.");
    }
}