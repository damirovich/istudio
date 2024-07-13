
using ISTUDIO.Application.Features.Products.DTOs;
using ISTUDIO.Domain.EntityModel;

namespace ISTUDIO.Application.Features.Products.Commands.EditPhotosProducts;
using ResModel = Result;
public class EditAllPhotosProductCommand : IRequest<ResModel>
{
    public int ProductId { get; set; }
    public ICollection<ProductImagesDTO> Images { get; set; } = new List<ProductImagesDTO>();

    public class Handler : IRequestHandler<EditAllPhotosProductCommand, ResModel>
    {
        private readonly IAppDbContext _appDbContext;
        private readonly IMapper _mapper;
        private readonly IFileStoreService _fileStoreService;

        public Handler(IAppDbContext appDbContext, IMapper mapper, IFileStoreService fileStoreService)
            => (_appDbContext, _mapper, _fileStoreService) = (appDbContext, mapper, fileStoreService);

        public async Task<ResModel> Handle(EditAllPhotosProductCommand command, CancellationToken cancellationToken)
        {
            var product = await _appDbContext.Products.Include(p => p.Images).FirstOrDefaultAsync(p => p.Id == command.ProductId, cancellationToken);

            if (product == null)
            {
                return ResModel.Failure(new[] { "Продукт не найден" });
            }

            // Удалить старые фотографии
            foreach (var image in product.Images)
            {
                _fileStoreService.DeleteImage(image.Url);
            }

            // Добавить новые фотографии
            product.Images.Clear();
            foreach (var imageDto in command.Images)
            {
                var newImage = _mapper.Map<ProductImagesEntity>(imageDto);
                product.Images.Add(newImage);
            }

            // Сохранение всех изменений в базе данных
            await _appDbContext.SaveChangesAsync(cancellationToken);

            return ResModel.Success();
        }
    }
}
