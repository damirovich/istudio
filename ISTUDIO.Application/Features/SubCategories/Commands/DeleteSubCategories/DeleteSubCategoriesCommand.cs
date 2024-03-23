namespace ISTUDIO.Application.Features.SubCategories.Commands.DeleteSubCategories;

using ResModel = Result;
public class DeleteSubCategoriesCommand : IRequest<ResModel>
{
    public int SubCategoryId {  get; set; }

    public class Handler : IRequestHandler<DeleteSubCategoriesCommand, ResModel>
    {
        private readonly IAppDbContext _appDbContext;
        private readonly IFileStoreService _fileStoreService;
        public Handler(IAppDbContext appDbContext, IFileStoreService fileStoreService) =>
                      (_appDbContext, _fileStoreService) = (appDbContext, fileStoreService);

        public async Task<ResModel> Handle(DeleteSubCategoriesCommand command, CancellationToken cancellationToken)
        {
            try
            {
                var existingSubCategory = await _appDbContext.Categories.FindAsync(command.SubCategoryId);

                if (existingSubCategory == null)
                    return ResModel.Failure(new[] { "Под категория не найдена" });

                _appDbContext.Categories.Remove(existingSubCategory);
                await _appDbContext.SaveChangesAsync(cancellationToken);

                _fileStoreService.DeleteImage(existingSubCategory.ImageUrl);

                return ResModel.Success();
            }
            catch (Exception ex)
            {
                return ResModel.Failure(new[] { ex.InnerException?.Message ?? ex.Message });
            }
        }
    }
}
