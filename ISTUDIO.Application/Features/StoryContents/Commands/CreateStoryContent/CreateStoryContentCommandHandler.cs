using ISTUDIO.Domain.EntityModel;

namespace ISTUDIO.Application.Features.StoryContents.Commands.CreateStoryContent;
using ResModel = Result;
public class CreateStoryContentCommandHandler : IRequestHandler<CreateStoryContentCommand, ResModel>
{
    private readonly IAppDbContext _appDbContext;
    private readonly IFileStoreService _fileStoreService;

    public CreateStoryContentCommandHandler(IAppDbContext appDbContext, IFileStoreService fileStoreService) =>
        (_appDbContext, _fileStoreService) = (appDbContext, fileStoreService);

    public async Task<ResModel> Handle(CreateStoryContentCommand command, CancellationToken cancellationToken)
    {
        try
        {
            // Сохранение медиафайла
            string mediaUrl = await _fileStoreService.SaveImage(command.MediaData);

            // Создание нового контента
            var storyContent = new StoryContentEntity
            {
                StoryId = command.StoryId,
                MediaUrl = mediaUrl,
                Type = command.Type,
                Queue = command.Queue
            };

            await _appDbContext.StoryContents.AddAsync(storyContent, cancellationToken);
            await _appDbContext.SaveChangesAsync(cancellationToken);

            return ResModel.Success();
        }
        catch (Exception ex)
        {
            return ResModel.Failure(new[] { ex.InnerException?.Message ?? ex.Message });
        }
    }
}