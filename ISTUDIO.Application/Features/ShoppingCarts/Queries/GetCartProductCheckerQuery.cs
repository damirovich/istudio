namespace ISTUDIO.Application.Features.ShoppingCarts.Queries;

public class GetCartProductCheckerQuery : IRequest<bool>
{
    public int ProductId { get; set; }
    public string UserId {  get; set; }

    public class Handler : IRequestHandler<GetCartProductCheckerQuery, bool>
    {
        private readonly IAppDbContext _appDbContext;

        public Handler(IAppDbContext appDbContext) =>
            (_appDbContext) = (appDbContext);

        public async Task<bool> Handle(GetCartProductCheckerQuery query, CancellationToken cancellationToken)
        {
            // Ищем корзину пользователя с указанным ProductId
            var productExists = await _appDbContext.ShoppingCarts
                .Where(cart => cart.UserId == query.UserId && cart.IsDeleted == false)
                .AnyAsync(cart => cart.Products.Any(product => product.Id == query.ProductId), cancellationToken);

            return productExists;
        }
    }
}
