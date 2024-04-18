namespace ISTUDIO.Application.Features.Categories.Commands.EditCategories;

using ISTUDIO.Application.Common.Interfaces;
using ResModel = Result;
public class EditCategoriesCommandHandler : IRequestHandler<EditCategoriesCommand, ResModel>
{
    private readonly IAppDbContext _appDbContext;
    private readonly IMapper _mapper;
    private readonly IRedisCacheService _redisCacheService;
    private readonly IFileStoreService _fileStoreService;
    public EditCategoriesCommandHandler(IAppDbContext appDbContext, IMapper mapper,
        IRedisCacheService redisCacheService, IFileStoreService fileStoreService) =>
        (_appDbContext, _mapper, _redisCacheService, _fileStoreService) 
        = 
        (appDbContext, mapper, redisCacheService, fileStoreService);

    public async Task<ResModel> Handle(EditCategoriesCommand command, CancellationToken cancellationToken)
    {
        try
        {
            
            var existingCategory = await _appDbContext.Categories.FindAsync(command.Id);
            if (existingCategory == null)
                return ResModel.Failure(new[] { "Категория не найдена" });
            string photoFilePath = string.Empty;

            if (command.PhotoCategory != null)
            {
                photoFilePath = await _fileStoreService.SaveImage(command.PhotoCategory);
            }

            _mapper.Map(command, existingCategory);

            existingCategory.ImageUrl = photoFilePath;

            _appDbContext.Categories.Update(existingCategory);

            await _appDbContext.SaveChangesAsync(cancellationToken);

            // Сбрасываем кеш Redis после изменение данных категории
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
