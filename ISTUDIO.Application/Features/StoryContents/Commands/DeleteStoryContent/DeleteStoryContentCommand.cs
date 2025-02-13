namespace ISTUDIO.Application.Features.StoryContents.Commands.DeleteStoryContent;
using ResModel = Result;
public class DeleteStoryContentCommand : IRequest<ResModel>
{
    public int Id { get; set; }

    public class Handler : IRequestHandler<DeleteStoryContentCommand, ResModel>
    {
        private readonly IAppDbContext _appDbContext;
        private readonly IFileStoreService _fileStoreService;

        public Handler(IAppDbContext appDbContext, IFileStoreService fileStoreService)
            => (_appDbContext, _fileStoreService) = (appDbContext, fileStoreService);


        public async Task<ResModel> Handle(DeleteStoryContentCommand command, CancellationToken cancellationToken)
        {
            try
            {
                // Получение контента из базы данных
                var storyContent = await _appDbContext.StoryContents.FindAsync(command.Id);
                if (storyContent == null)
                {
                    return ResModel.Failure(new[] { "Контент не найден" });
                }

                // Удаление медиа
                if (!string.IsNullOrEmpty(storyContent.MediaUrl))
                {
                    _fileStoreService.DeleteImage(storyContent.MediaUrl);
                }

                // Удаление контента
                _appDbContext.StoryContents.Remove(storyContent);
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
