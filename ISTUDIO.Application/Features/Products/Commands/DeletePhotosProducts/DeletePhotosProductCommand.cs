namespace ISTUDIO.Application.Features.Products.Commands.DeletePhotosProducts;

using ResModel = Result;
public class DeletePhotosProductCommand : IRequest<ResModel>
{
    public int ProductImagesId { get; set; }

    public class Handler : IRequestHandler<DeletePhotosProductCommand, ResModel>
    {
        private readonly IAppDbContext _appDbContext;
        private readonly IFileStoreService _fileStoreService;
        public Handler(IAppDbContext appDbContext, IFileStoreService fileStoreService)
        {
            _appDbContext = appDbContext;
            _fileStoreService = fileStoreService;
        }

        public async Task<ResModel> Handle(DeletePhotosProductCommand command, CancellationToken cancellationToken)
        {
            try
            {
                var exstingPhotosProduct = await _appDbContext.ProductImages.FindAsync(command.ProductImagesId);

                if (exstingPhotosProduct == null)
                    return ResModel.Failure(new[] { "ProductImages не найден" });

                _appDbContext.ProductImages.Remove(exstingPhotosProduct);

                await _appDbContext.SaveChangesAsync(cancellationToken);

                if(exstingPhotosProduct != null)
                {
                    _fileStoreService.DeleteImage(exstingPhotosProduct.Url);
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
