namespace ISTUDIO.Application.Features.Categories.Commands.CreateCategories;

using ISTUDIO.Domain.EntityModel;
using ResModel = Result;
public class CreateCategoriesCommand : IRequest<ResModel>
{
    public string Name { get; set; }
    public string? Description { get; set; }
    public string? ImageUrl { get; set; }

    public class Handler : IRequestHandler<CreateCategoriesCommand, ResModel>
    {
        private readonly IAppDbContext _appDbContext;
        public Handler(IAppDbContext appDbContext) => _appDbContext = appDbContext;

        public async Task<ResModel> Handle(CreateCategoriesCommand command, CancellationToken cancellationToken)
        {
            try 
            {
                var category = new CategoryEntity
                {
                    Name = command.Name,
                    Description = command.Description,
                    ImageUrl = command.ImageUrl,
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
