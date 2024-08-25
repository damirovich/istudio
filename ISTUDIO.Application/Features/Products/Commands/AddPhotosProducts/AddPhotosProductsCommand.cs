namespace ISTUDIO.Application.Features.Products.Commands.AddPhotosProducts;

using ISTUDIO.Application.Features.Products.DTOs;
using ISTUDIO.Domain.EntityModel;
using ResModel = Result;
public class AddPhotosProductsCommand : IRequest<ResModel>
{
    public int ProductId { get; set; }
    public ProductImagesDTO Photo { get; set; }

    public class Handler : IRequestHandler<AddPhotosProductsCommand, ResModel>
    {
        private readonly IAppDbContext _appDbContext;
        private readonly IMapper _mapper;        

        public Handler(IAppDbContext appDbContext, IMapper mapper)
            => (_appDbContext, _mapper) = (appDbContext, mapper);

        public async Task<ResModel> Handle(AddPhotosProductsCommand command, CancellationToken cancellationToken)
        {
            if (command.Photo == null)
            {
                return ResModel.Failure(new[] { "Фото не может быть пустым" });
            }

            var exsistingProduct = await _appDbContext.Products.FindAsync(command.ProductId);

            if(exsistingProduct == null)
            {
                return ResModel.Failure(new[] { "Продукт не найден" });
            }

            // Маппинг DTO на сущность
            var newImage = _mapper.Map<ProductImagesEntity>(command.Photo);
            
            newImage.ProductId = command.ProductId;
            // Добавляем изображение в базу данных
            await _appDbContext.ProductImages.AddAsync(newImage, cancellationToken);
            await _appDbContext.SaveChangesAsync(cancellationToken);

            return ResModel.Success();
        }
     }
}
