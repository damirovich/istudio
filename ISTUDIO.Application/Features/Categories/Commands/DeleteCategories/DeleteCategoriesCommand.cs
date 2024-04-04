namespace ISTUDIO.Application.Features.Categories.Commands.DeleteCategories;
using ResModel = Result;
public class DeleteCategoriesCommand : IRequest<ResModel>
{
    public int CategoryId { get; set; }
    public class Handler : IRequestHandler<DeleteCategoriesCommand, ResModel>
    {
        private readonly IAppDbContext _appDbContext;
        private readonly IFileStoreService _fileStoreService;
        private readonly IRedisCacheService _redisCacheService;
        public Handler(IAppDbContext appDbContext, IFileStoreService fileStoreService, IRedisCacheService redisCacheService) =>
            (_appDbContext, _fileStoreService, _redisCacheService) = (appDbContext, fileStoreService, redisCacheService);

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

                // Сбрасываем кеш Redis после Удалении категории
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
