namespace ISTUDIO.Application.Features.Banners.Commands.DeleteBannes;
using ResModel = Result;
public class DeleteBannerCommand : IRequest<ResModel>
{
    public int BannerId { get; set; }

    public class Handler : IRequestHandler<DeleteBannerCommand, ResModel>
    {
        private readonly IAppDbContext _appDbContext;
        private readonly IRedisCacheService _redisCacheService;
        private readonly IFileStoreService _fileStoreService;

        public Handler(IAppDbContext appDbContext, IRedisCacheService redisCacheService, IFileStoreService fileStoreService) =>
            (_appDbContext, _redisCacheService, _fileStoreService) = (appDbContext, redisCacheService, fileStoreService);

        public async Task<ResModel> Handle(DeleteBannerCommand command, CancellationToken cancellationToken)
        {
            try
            {
                var banner = await _appDbContext.Banners.FindAsync(command.BannerId);

                if (banner == null)
                {
                    return ResModel.Failure(new[] { "Banner not found." });
                }

                _appDbContext.Banners.Remove(banner);
                await _appDbContext.SaveChangesAsync(cancellationToken);

                // Remove associated image from file store
                if (!string.IsNullOrEmpty(banner.PhotoUrl))
                {
                     _fileStoreService.DeleteImage(banner.PhotoUrl);
                }

                // Clear Redis cache
                string cacheKey = "BannersIstudio";
                await _redisCacheService.RemoveAsync(cacheKey);

                return ResModel.Success();
            }
            catch (Exception ex)
            {
                return ResModel.Failure(new[] { ex.InnerException?.Message ?? ex.Message });
            }
        }
    }
}
