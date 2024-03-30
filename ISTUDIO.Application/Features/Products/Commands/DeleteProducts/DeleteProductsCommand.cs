namespace ISTUDIO.Application.Features.Products.Commands.DeleteProducts;

using ISTUDIO.Application.Common.Interfaces;
using ResModel = Result;
public class DeleteProductsCommand : IRequest<ResModel>
{
    public int ProductId { get; set; }

    public class Handler : IRequestHandler<DeleteProductsCommand, ResModel>
    {
        private readonly IAppDbContext _appDbContext;
        private readonly IFileStoreService _fileStoreService;
        public Handler(IAppDbContext appDbContext, IFileStoreService fileStoreService)
        {
            _appDbContext = appDbContext;
            _fileStoreService = fileStoreService;
        }

        public async Task<ResModel> Handle(DeleteProductsCommand command, CancellationToken cancellationToken)
        {
            try
            {
                var existingProduct = await _appDbContext.Products.Include(c => c.Images)
                          .FirstOrDefaultAsync(c => c.Id == command.ProductId);
                if (existingProduct == null)
                    return ResModel.Failure(new[] { "Product не найдена" });

                _appDbContext.Products.Remove(existingProduct);

                await _appDbContext.SaveChangesAsync(cancellationToken);

                if (existingProduct.Images != null)
                {
                    foreach (var item in existingProduct.Images)
                    {
                        _fileStoreService.DeleteImage(item.Url);
                    }
                }


                return ResModel.Success();
            }
            catch (Exception ex)
            {
                return ResModel.Failure(new[] { ex.InnerException?.Message ?? ex.Message });
            }
        }
    }
}
