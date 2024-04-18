namespace ISTUDIO.Application.Features.SubCategories.Commands.CreateSubCategories;

using ISTUDIO.Application.Common.Interfaces;
using ISTUDIO.Domain.EntityModel;
using ResModel = Result;
public class CreateSubCategoriesCommand : IRequest<ResModel>
{
    public string Name { get; set; }
    public string? Description { get; set; }
    public byte[]? PhotoCategory { get; set; }
    public int CategoryId { get; set; }

    public class Handler : IRequestHandler<CreateSubCategoriesCommand, ResModel>
    {
        private readonly IAppDbContext _appDbContext;
        private readonly IRedisCacheService _redisCacheService;
        private readonly IFileStoreService _fileStoreService;
        public Handler(IAppDbContext appDbContext, IRedisCacheService redisCacheService, IFileStoreService fileStoreService) =>
                (_appDbContext, _redisCacheService,_fileStoreService) = (appDbContext, redisCacheService, fileStoreService);

        public async Task<ResModel> Handle(CreateSubCategoriesCommand command, CancellationToken cancellationToken)
        {
            try
            {
                string photoFilePath = string.Empty;

                if (command.PhotoCategory != null)
                {
                    photoFilePath = await _fileStoreService.SaveImage(command.PhotoCategory);
                }
                var subCategory = new CategoryEntity
                {
                    Name = command.Name,
                    Description = command.Description,
                    ImageUrl = photoFilePath,
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
