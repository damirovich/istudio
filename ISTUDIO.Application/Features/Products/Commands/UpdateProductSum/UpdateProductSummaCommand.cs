namespace ISTUDIO.Application.Features.Products.Commands.UpdateProductSum;
using ResModel = Result;
public class UpdateProductSummaCommand : IRequest<ResModel>
{
    public int ProductId { get; set; }
    public decimal ProductSumma { get; set; }

    public class Handler : IRequestHandler<UpdateProductSummaCommand, ResModel>
    {
        private readonly IAppDbContext _appDbContext;

        public Handler(IAppDbContext appDbContext) =>
            (_appDbContext) = (appDbContext);

        public async Task<ResModel> Handle(UpdateProductSummaCommand command, CancellationToken cancellationToken)
        {
            try
            {
                var product = await _appDbContext.Products.FindAsync(command.ProductId);

                if (product == null)
                {
                    return ResModel.Failure(new[] { "Продукт не найден." });
                }

                product.Price = command.ProductSumma;
                product.CreateDate = DateTime.Now;

                _appDbContext.Products.Update(product);
                await _appDbContext.SaveChangesAsync(cancellationToken);

                return ResModel.Success();
            }
            catch (Exception ex)
            {
                return ResModel.Failure(new[] { $"Ошибка при обновлении сумму продукта: {ex.Message}" });
            }
        }
    }
}
