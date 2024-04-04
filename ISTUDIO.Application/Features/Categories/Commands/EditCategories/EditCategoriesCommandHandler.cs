namespace ISTUDIO.Application.Features.Categories.Commands.EditCategories;
using ResModel = Result;
public class EditCategoriesCommandHandler : IRequestHandler<EditCategoriesCommand, ResModel>
{
    private readonly IAppDbContext _appDbContext;
    private readonly IMapper _mapper;
    private readonly IRedisCacheService _redisCacheService;
    public EditCategoriesCommandHandler(IAppDbContext appDbContext, IMapper mapper, IRedisCacheService redisCacheService) =>
        (_appDbContext, _mapper, _redisCacheService) = (appDbContext, mapper, redisCacheService);

    public async Task<ResModel> Handle(EditCategoriesCommand command, CancellationToken cancellationToken)
    {
        try
        {
            
            var existingCategory = await _appDbContext.Categories.FindAsync(command.Id);
            if (existingCategory == null)
                return ResModel.Failure(new[] { "Категория не найдена" });

            _mapper.Map(command, existingCategory);

            existingCategory.ImageUrl = command.PhotoFilePath;

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
