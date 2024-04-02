
namespace ISTUDIO.Application.Features.ShoppingCarts.Commands.DeleteShoppingCarts;

using System.ComponentModel.DataAnnotations;
using ResModel = Result;
public class ClearShoppingCartsCommand : IRequest<ResModel>
{
    [Required]
    public string UserId { get; set; }

    public class Handler : IRequestHandler<ClearShoppingCartsCommand, ResModel>
    {
        private readonly IAppDbContext _appDbContext;
        public Handler(IAppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public async Task<ResModel> Handle(ClearShoppingCartsCommand command, CancellationToken cancellationToken)
        {
            try
            {
                var existingProductCarts = await _appDbContext.ShoppingCarts
                        .Where(c => c.UserId == command.UserId)
                        .ToListAsync();

                if (existingProductCarts == null || existingProductCarts.Count == 0)
                    return ResModel.Failure(new[] { "Shopping carts not found for the user" });

                _appDbContext.ShoppingCarts.RemoveRange(existingProductCarts);

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
public class ClearShoppingCartsCommandValidator : AbstractValidator<ClearShoppingCartsCommand>
{
    public ClearShoppingCartsCommandValidator()
    {

        RuleFor(v => v.UserId)
               .NotEmpty().WithMessage("UserId не может быть пустым.")
               .MaximumLength(255).WithMessage("UserId должно быть не длиннее 255 символов.");

    }
}
