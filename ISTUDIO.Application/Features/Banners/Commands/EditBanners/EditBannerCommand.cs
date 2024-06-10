namespace ISTUDIO.Application.Features.Banners.Commands.EditBanners;

using ResModel = Result;
public class EditBannerCommand : IRequest<ResModel>
{
    public int BannerId { get; set; }
    public byte[] PhotoBanner { get; set; }
    public int Status { get; set; }
    public int? CategoryId { get; set; }
    public int? DiscountId { get; set; }
    public int? ProductId { get; set; }
    public class Handler : IRequestHandler<EditBannerCommand, ResModel>
    {
        private readonly IAppDbContext _appDbContext;
        private readonly IRedisCacheService _redisCacheService;
        private readonly IFileStoreService _fileStoreService;

        public Handler(IAppDbContext appDbContext, IRedisCacheService redisCacheService, IFileStoreService fileStoreService) =>
            (_appDbContext, _redisCacheService, _fileStoreService) = (appDbContext, redisCacheService, fileStoreService);

        public async Task<ResModel> Handle(EditBannerCommand command, CancellationToken cancellationToken)
        {
            try
            {
                var banner = await _appDbContext.Banners.FindAsync(command.BannerId);

                if (banner == null)
                {
                    return ResModel.Failure(new[] { "Banner not found." });
                }

                if (command.PhotoBanner != null && command.PhotoBanner.Length > 0)
                {
                    if (!string.IsNullOrEmpty(banner.PhotoUrl))
                    {
                        _fileStoreService.DeleteImage(banner.PhotoUrl);
                    }

                    banner.PhotoUrl = await _fileStoreService.SaveImage(command.PhotoBanner);
                }

                banner.Status = command.Status;
                banner.CategoryId = command.CategoryId;
                banner.DiscountId = command.DiscountId;
                banner.ProductId = command.ProductId;

                _appDbContext.Banners.Update(banner);
                await _appDbContext.SaveChangesAsync(cancellationToken);

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
