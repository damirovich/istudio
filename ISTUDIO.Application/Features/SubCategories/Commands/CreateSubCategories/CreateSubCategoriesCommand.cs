namespace ISTUDIO.Application.Features.SubCategories.Commands.CreateSubCategories;

using ISTUDIO.Domain.EntityModel;
using ResModel = Result;
public class CreateSubCategoriesCommand : IRequest<ResModel>
{
    public string Name { get; set; }
    public string? Description { get; set; }
    public string? ImageUrl { get; set; }
    public int CategoryId { get; set; }

    public class Handler : IRequestHandler<CreateSubCategoriesCommand, ResModel>
    {
        private readonly IAppDbContext _appDbContext;
        public Handler(IAppDbContext appDbContext) => _appDbContext = appDbContext;

        public async Task<ResModel> Handle(CreateSubCategoriesCommand command, CancellationToken cancellationToken)
        {
            try
            {
                var subCategory = new CategoryEntity
                {
                    Name = command.Name,
                    Description = command.Description,
                    ImageUrl = command.ImageUrl,
                    ParentCategoryId = command.CategoryId
                };
                await _appDbContext.Categories.AddAsync(subCategory);
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
