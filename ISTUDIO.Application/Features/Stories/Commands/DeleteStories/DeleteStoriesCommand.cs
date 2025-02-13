namespace ISTUDIO.Application.Features.Stories.Commands.DeleteStories;
using ResModel = Result;
public class DeleteStoriesCommand : IRequest<ResModel>
{
    public int StoriesId { get; set; }

    public class DeleteStoriesCommandHandler : IRequestHandler<DeleteStoriesCommand, ResModel>
    {
        private readonly IAppDbContext _appDbContext;
        private readonly IFileStoreService _fileStoreService;

        public DeleteStoriesCommandHandler(IAppDbContext appDbContext, IFileStoreService fileStoreService) =>
            (_appDbContext, _fileStoreService) = (appDbContext, fileStoreService);

        public async Task<ResModel> Handle(DeleteStoriesCommand command, CancellationToken cancellationToken)
        {
            try
            {
                // Получение сторис из базы данных
                var story = await _appDbContext.Stories.FindAsync(command.StoriesId);
                if (story == null)
                {
                    return ResModel.Failure(new[] { "Сторис не найдена" });
                }

                // Удаление иконки из хранилища
                if (!string.IsNullOrEmpty(story.IconUrl))
                {
                    _fileStoreService.DeleteImage(story.IconUrl);
                }

                // Удаление сторис из базы данных
                _appDbContext.Stories.Remove(story);
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
