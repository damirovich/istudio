
namespace ISTUDIO.Application.Features.Products.Commands.EditPhotosProducts;
using ISTUDIO.Application.Features.Products.DTOs;

using ISTUDIO.Domain.EntityModel;
using ResModel = Result;
public class EditPhotosProductCommand : IRequest<ResModel>
{
    public int Id { get; set; }  // Id изображения, а не продукта
    public ProductImagesDTO Photo { get; set; }

    public class Handler : IRequestHandler<EditPhotosProductCommand, ResModel>
    {
        private readonly IAppDbContext _appDbContext;
        private readonly IMapper _mapper;
        private readonly IFileStoreService _fileStoreService;

        public Handler(IAppDbContext appDbContext, IMapper mapper, IFileStoreService fileStoreService)
            => (_appDbContext, _mapper, _fileStoreService) = (appDbContext, mapper, fileStoreService);

        public async Task<ResModel> Handle(EditPhotosProductCommand command, CancellationToken cancellationToken)
        {
            // Получаем изображение по Id, а не продукт
            var existingImage = await _appDbContext.ProductImages
                 .FirstOrDefaultAsync(img => img.Id == command.Id, cancellationToken);

            if (existingImage == null)
            {
                return ResModel.Failure(new[] { "Изображение не найдено" });
            }

            // Проверяем, изменился ли URL, и если да, то обновляем его и удаляем старый файл
            if (!string.Equals(existingImage.Url, command.Photo.Url, StringComparison.OrdinalIgnoreCase))
            {
                _fileStoreService.DeleteImage(existingImage.Url); // Удаление старого файла из файлового хранилища
                existingImage.Url = command.Photo.Url; // Обновляем URL в базе данных
                await _appDbContext.SaveChangesAsync(cancellationToken); // Сохраняем изменения
            }

            return ResModel.Success();
        }
    }
}
