namespace ISTUDIO.Application.Features.Stories.Commands.CreateStories;

using ISTUDIO.Domain.EntityModel;
using ResModel = Result;
public class CreateStoriesCommandHandler : IRequestHandler<CreateStoriesCommand, ResModel>
{
    private readonly IAppDbContext _appDbContext;
    private readonly IFileStoreService _fileStoreService;

    public CreateStoriesCommandHandler(IAppDbContext appDbContext, IFileStoreService fileStoreService) =>
        (_appDbContext, _fileStoreService) = (appDbContext, fileStoreService);

    public async Task<ResModel> Handle(CreateStoriesCommand command, CancellationToken cancellationToken)
    {
        try
        {
            // Сохранение иконки в файловом хранилище
            string iconPhotoPath = string.Empty;
            if (command.IconPhoto != null && command.IconPhoto.Length > 0)
            {
                iconPhotoPath = await _fileStoreService.SaveImage(command.IconPhoto);
            }

            // Создание новой сторис
            var story = new StoriesEntity
            {
                IconUrl = iconPhotoPath,
                IsActive = command.IsActive,
                CreatedAt = command.CreatedAt,
                ExpireAt = command.ExpireAt
            };

            // Сохранение в базу данных
            await _appDbContext.Stories.AddAsync(story, cancellationToken);
            await _appDbContext.SaveChangesAsync(cancellationToken);

            return ResModel.Success();
        }
        catch (Exception ex)
        {
            return ResModel.Failure(new[] { ex.InnerException?.Message ?? ex.Message });
        }
    }
}
