namespace ISTUDIO.Application.Features.Products.Commands.UpdateProductQuantity;
using ResModel = Result;
public class UpdateProductQuantityCommand : IRequest<ResModel>
{
    public int ProductId { get; set; }
    public int ProductQuantity { get; set; }

    public class Handler : IRequestHandler<UpdateProductQuantityCommand, ResModel>
    {
        private readonly IAppDbContext _appDbContext;

        public Handler(IAppDbContext appDbContext)=>
            (_appDbContext) = (appDbContext);

        public async Task<ResModel> Handle(UpdateProductQuantityCommand command, CancellationToken cancellationToken)
        {
            try
            {
                var product = await _appDbContext.Products.FindAsync(command.ProductId);

                if (product == null)
                {
                    return ResModel.Failure(new[] { "Продукт не найден." });
                }

                product.QuantityInStock = command.ProductQuantity;
                product.CreateDate = DateTime.Now;
                
                _appDbContext.Products.Update(product);
                await _appDbContext.SaveChangesAsync(cancellationToken);

                return ResModel.Success();
            }
            catch (Exception ex)
            {
                return ResModel.Failure(new[] { $"Ошибка при обновлении количество продукта: {ex.Message}" });
            }
        }
    }
}
