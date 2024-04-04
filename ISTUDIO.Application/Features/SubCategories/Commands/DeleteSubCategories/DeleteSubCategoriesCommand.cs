namespace ISTUDIO.Application.Features.SubCategories.Commands.DeleteSubCategories;

using ResModel = Result;
public class DeleteSubCategoriesCommand : IRequest<ResModel>
{
    public int SubCategoryId {  get; set; }

    public class Handler : IRequestHandler<DeleteSubCategoriesCommand, ResModel>
    {
        private readonly IAppDbContext _appDbContext;
        private readonly IFileStoreService _fileStoreService;
        private readonly IRedisCacheService _redisCacheService;
        public Handler(IAppDbContext appDbContext, IFileStoreService fileStoreService, IRedisCacheService redisCacheService) =>
                      (_appDbContext, _fileStoreService, _redisCacheService) = (appDbContext, fileStoreService, redisCacheService);

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


                // Сбрасываем кеш Redis после удаление SubCategory
                string cashKey = "CategoriesIstudio";
                await _redisCacheService.RemoveAsync(cashKey);

                return ResModel.Success();
            }
            catch (Exception ex)
            {
                return ResModel.Failure(new[] { ex.InnerException?.Message ?? ex.Message });
            }
        }
    }
}
