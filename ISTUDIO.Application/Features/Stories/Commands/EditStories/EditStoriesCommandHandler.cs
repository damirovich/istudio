namespace ISTUDIO.Application.Features.Stories.Commands.EditStories;
using ResModel = Result;
public class EditStoriesCommandHandler : IRequestHandler<EditStoriesCommand, ResModel>
{
    private readonly IAppDbContext _appDbContext;
    private readonly IFileStoreService _fileStoreService;

    public EditStoriesCommandHandler(IAppDbContext appDbContext, IFileStoreService fileStoreService) =>
        (_appDbContext, _fileStoreService) = (appDbContext, fileStoreService);

    public async Task<ResModel> Handle(EditStoriesCommand command, CancellationToken cancellationToken)
    {
        try
        {
            // Получение сторис из базы данных
            var story = await _appDbContext.Stories.FindAsync(command.Id);
            if (story == null)
            {
                return ResModel.Failure(new[] { "Сторис не найдена" });
            }

            // Если передана новая иконка
            if (command.IconPhoto != null && command.IconPhoto.Length > 0)
            {
                // Удаление старой иконки
                if (!string.IsNullOrEmpty(story.IconUrl))
                {
                    _fileStoreService.DeleteImage(story.IconUrl);
                }

                // Сохранение новой иконки
                story.IconUrl = await _fileStoreService.SaveImage(command.IconPhoto);
            }

            // Обновление данных
            story.IsActive = command.IsActive;
            story.ExpireAt = command.ExpireAt;

            await _appDbContext.SaveChangesAsync(cancellationToken);

            return ResModel.Success();
        }
        catch (Exception ex)
        {
            return ResModel.Failure(new[] { ex.InnerException?.Message ?? ex.Message });
        }
    }
}
