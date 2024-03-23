namespace ISTUDIO.Application.Features.Categories.Commands.DeleteCategories;
using ResModel = Result;
public class DeleteCategoriesCommand : IRequest<ResModel>
{
    public int CategoryId { get; set; }
    public class Handler : IRequestHandler<DeleteCategoriesCommand, ResModel>
    {
        private readonly IAppDbContext _appDbContext;
        private readonly IFileStoreService _fileStoreService;
        public Handler(IAppDbContext appDbContext, IFileStoreService fileStoreService) =>
            (_appDbContext, _fileStoreService) = (appDbContext, fileStoreService);

        public async Task<ResModel> Handle(DeleteCategoriesCommand command, CancellationToken cancellationToken)
        {
            try
            {
                var existingCategory = await _appDbContext.Categories.FindAsync(command.CategoryId);
                if (existingCategory == null)
                    return ResModel.Failure(new[] { "Категория не найдена" });

                _appDbContext.Categories.Remove(existingCategory);
                await _appDbContext.SaveChangesAsync(cancellationToken);

                _fileStoreService.DeleteImage(existingCategory.ImageUrl);
                 
                return ResModel.Success();
            }
            catch (Exception ex)
            {
                return ResModel.Failure(new[] { ex.InnerException?.Message ?? ex.Message });
            }
        }
    }
}
