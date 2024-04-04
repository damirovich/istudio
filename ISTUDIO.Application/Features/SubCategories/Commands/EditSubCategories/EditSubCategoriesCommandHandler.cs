namespace ISTUDIO.Application.Features.SubCategories.Commands.EditSubCategories;

using ResModel = Result;
public class EditSubCategoriesCommandHandler : IRequestHandler<EditSubCategoriesCommand, ResModel>
{

    private readonly IAppDbContext _appDbContext;
    private readonly IMapper _mapper;
    private readonly IRedisCacheService _redisCacheService;
    public EditSubCategoriesCommandHandler(IAppDbContext appDbContext, IMapper mapper, IRedisCacheService redisCacheService) =>
        (_appDbContext, _mapper, _redisCacheService) = (appDbContext, mapper, redisCacheService);

    public async Task<ResModel> Handle(EditSubCategoriesCommand command, CancellationToken cancellationToken)
    {
        try
        {
            var existingSubCategory = await _appDbContext.Categories.FindAsync(command.Id);
            if (existingSubCategory == null)
                return ResModel.Failure(new[] { "Под категория не найдена" });

            _mapper.Map(command, existingSubCategory);

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
