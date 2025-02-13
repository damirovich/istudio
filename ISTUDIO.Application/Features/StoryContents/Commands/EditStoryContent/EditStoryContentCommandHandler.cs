namespace ISTUDIO.Application.Features.StoryContents.Commands.EditStoryContent;
using ResModel = Result;
public class EditStoryContentCommandHandler : IRequestHandler<EditStoryContentCommand, ResModel>
{
    private readonly IAppDbContext _appDbContext;
    private readonly IFileStoreService _fileStoreService;

    public EditStoryContentCommandHandler(IAppDbContext appDbContext, IFileStoreService fileStoreService) =>
        (_appDbContext, _fileStoreService) = (appDbContext, fileStoreService);

    public async Task<ResModel> Handle(EditStoryContentCommand command, CancellationToken cancellationToken)
    {
        try
        {
            // Получение контента из базы данных
            var storyContent = await _appDbContext.StoryContents.FindAsync(command.Id);
            if (storyContent == null)
            {
                return ResModel.Failure(new[] { "Контент не найден" });
            }

            // Обновление медиа (если передано)
            if (command.MediaData != null && command.MediaData.Length > 0)
            {
                _fileStoreService.DeleteImage(storyContent.MediaUrl); // Удаляем старое медиа
                storyContent.MediaUrl = await _fileStoreService.SaveImage(command.MediaData);
            }

            // Обновление типа и очередности
            storyContent.Type = command.Type;
            storyContent.Queue = command.Queue;

            await _appDbContext.SaveChangesAsync(cancellationToken);

            return ResModel.Success();
        }
        catch (Exception ex)
        {
            return ResModel.Failure(new[] { ex.InnerException?.Message ?? ex.Message });
        }
    }
}