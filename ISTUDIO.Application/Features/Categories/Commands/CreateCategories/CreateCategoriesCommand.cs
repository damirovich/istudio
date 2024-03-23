namespace ISTUDIO.Application.Features.Categories.Commands.CreateCategories;

using ISTUDIO.Application.Common.Interfaces;
using ISTUDIO.Domain.EntityModel;
using ResModel = Result;
public class CreateCategoriesCommand : IRequest<ResModel>
{
    public string Name { get; set; }
    public string? Description { get; set; }
    public string? PhotoFilePath { get; set; }

    public class Handler : IRequestHandler<CreateCategoriesCommand, ResModel>
    {
        private readonly IAppDbContext _appDbContext;
        private readonly IFileStoreService _fileStoreService;
        public Handler(IAppDbContext appDbContext, IFileStoreService fileStoreService) =>
                    (_appDbContext, _fileStoreService) = (appDbContext, fileStoreService);

        public async Task<ResModel> Handle(CreateCategoriesCommand command, CancellationToken cancellationToken)
        {
            try 
            {
               
                var category = new CategoryEntity
                {
                    Name = command.Name,
                    Description = command.Description,
                    ImageUrl = command.PhotoFilePath,
                };
                await _appDbContext.Categories.AddAsync(category);
                await _appDbContext.SaveChangesAsync(cancellationToken);

                return ResModel.Success();
            }
            catch (Exception ex)
            {
               return ResModel.Failure(new[] { ex.InnerException?.Message ?? ex.Message });
            }
        }
    }
}
