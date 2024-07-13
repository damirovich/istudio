namespace ISTUDIO.Application.Features.Products.Commands.CreateProducts;

using ISTUDIO.Domain.EntityModel;
using ResModel = Result;
public class CreateProductsCommandHandler : IRequestHandler<CreateProductsCommand, ResModel>
{
    private readonly IAppDbContext _appDbContext;
    private readonly IMapper _mapper;
    public CreateProductsCommandHandler(IAppDbContext appDbContext, IMapper mapper)
        => (_appDbContext, _mapper) = (appDbContext, mapper);

    public async Task<ResModel> Handle(CreateProductsCommand command, CancellationToken cancellationToken)
    {
        try
        {
            // Маппинг команды на сущность
            var productEntity = _mapper.Map<ProductsEntity>(command);
            productEntity.CreateDate = DateTime.UtcNow;
            // Добавление сущности в контекст базы данных
            _appDbContext.Products.Add(productEntity);
            
            // Сохранение всех изменений в базе данных одновременно
            await _appDbContext.SaveChangesAsync(cancellationToken);

            return ResModel.Success();
        }
        catch (Exception ex)
        {
            return ResModel.Failure(new[] { ex.Message });
        }
    }
}
