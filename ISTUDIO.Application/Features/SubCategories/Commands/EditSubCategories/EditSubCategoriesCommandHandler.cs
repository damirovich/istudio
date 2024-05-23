namespace ISTUDIO.Application.Features.SubCategories.Commands.EditSubCategories;

using ISTUDIO.Application.Common.Interfaces;
using ResModel = Result;
public class EditSubCategoriesCommandHandler : IRequestHandler<EditSubCategoriesCommand, ResModel>
{

    private readonly IAppDbContext _appDbContext;
    private readonly IMapper _mapper;
    private readonly IRedisCacheService _redisCacheService;
    private readonly IFileStoreService _fileStoreService;
    public EditSubCategoriesCommandHandler(IAppDbContext appDbContext, IMapper mapper,
        IRedisCacheService redisCacheService, IFileStoreService fileStoreService) =>
        (_appDbContext, _mapper, _redisCacheService, _fileStoreService)
        =
        (appDbContext, mapper, redisCacheService, fileStoreService);

    public async Task<ResModel> Handle(EditSubCategoriesCommand command, CancellationToken cancellationToken)
    {
        try
        {
            var existingSubCategory = await _appDbContext.Categories.FindAsync(command.Id);
            if (existingSubCategory == null)
                return ResModel.Failure(new[] { "Под категория не найдена" });

            string photoFilePath = string.Empty;
            string iconPhotoFilePath = string.Empty;
            if (command.PhotoCategory != null && command.PhotoCategory.Length > 0) 
            {
                photoFilePath = await _fileStoreService.SaveImage(command.PhotoCategory);
            }
            if(command.IconPhotoCategory != null && command.IconPhotoCategory.Length > 0)
            {
                iconPhotoFilePath = await _fileStoreService.SaveImage(command.IconPhotoCategory);
            }
            _mapper.Map(command, existingSubCategory);


            existingSubCategory.ImageUrl = photoFilePath;
            existingSubCategory.IconImageUrl = iconPhotoFilePath;

            _appDbContext.Categories.Update(existingSubCategory);

            await _appDbContext.SaveChangesAsync(cancellationToken);

            // Сбрасываем кеш Redis после изменение SubCategory
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
