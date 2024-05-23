namespace ISTUDIO.Application.Features.Banners.Commands.CreateBanners;

using ISTUDIO.Domain.EntityModel;
using ResModel = Result;
public class CreateBannersCommand : IRequest<ResModel>
{
    public byte[] PhotoBanner { get; set; }
    public int Status { get; set; }
    public int? CategoryId { get; set; }
    public int? DiscountId { get; set; }
    public int? ProductId { get; set; }
    public class Handler : IRequestHandler<CreateBannersCommand, ResModel>
    {
        private readonly IAppDbContext _appDbContext;
        private readonly IRedisCacheService _redisCacheService;
        private readonly IFileStoreService _fileStoreService;
        public Handler(IAppDbContext appDbContext, IRedisCacheService redisCacheService, IFileStoreService fileStoreService) =>
                    (_appDbContext, _redisCacheService, _fileStoreService) = (appDbContext, redisCacheService, fileStoreService);

        public async Task<ResModel> Handle(CreateBannersCommand command, CancellationToken cancellationToken)
        {
            try
            {
                string photoFilePath = string.Empty;

                if (command.PhotoBanner != null && command.PhotoBanner.Length > 0)
                {
                    photoFilePath = await _fileStoreService.SaveImage(command.PhotoBanner);
                }
               
                // Добавление новой категории
                var banner = new BannerEntity
                {
                    PhotoUrl = photoFilePath,
                    Status = command.Status,
                    CategoryId = command.CategoryId,
                    DiscountId = command.DiscountId,
                    ProductId = command.ProductId
                };
                await _appDbContext.Banners.AddAsync(banner);
                await _appDbContext.SaveChangesAsync(cancellationToken);

                // Сбрасываем кеш Redis после добавления новой категории
                string cashKey = "BannersIstudio";
                await _redisCacheService.RemoveAsync(cashKey);

                return ResModel.Success();
            }
            catch (Exception ex)
            {
                return ResModel.Failure(new[] { ex.InnerException?.Message ?? ex.Message });
            }
        }
    }
}
