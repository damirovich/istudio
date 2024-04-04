namespace ISTUDIO.Application.Features.SubCategories.Commands.CreateSubCategories;

using ISTUDIO.Application.Common.Interfaces;
using ISTUDIO.Domain.EntityModel;
using ResModel = Result;
public class CreateSubCategoriesCommand : IRequest<ResModel>
{
    public string Name { get; set; }
    public string? Description { get; set; }
    public string? PhotoFilePath { get; set; }
    public int CategoryId { get; set; }

    public class Handler : IRequestHandler<CreateSubCategoriesCommand, ResModel>
    {
        private readonly IAppDbContext _appDbContext;
        private readonly IRedisCacheService _redisCacheService;
        public Handler(IAppDbContext appDbContext, IRedisCacheService redisCacheService) =>
                (_appDbContext, _redisCacheService) = (appDbContext, redisCacheService);

        public async Task<ResModel> Handle(CreateSubCategoriesCommand command, CancellationToken cancellationToken)
        {
            try
            {
                var subCategory = new CategoryEntity
                {
                    Name = command.Name,
                    Description = command.Description,
                    ImageUrl = command.PhotoFilePath,
                    ParentCategoryId = command.CategoryId
                };
                await _appDbContext.Categories.AddAsync(subCategory);
                await _appDbContext.SaveChangesAsync(cancellationToken);

                // Сбрасываем кеш Redis после Добавление SubCategory
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
