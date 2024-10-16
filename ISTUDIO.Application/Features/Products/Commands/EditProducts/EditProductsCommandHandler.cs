namespace ISTUDIO.Application.Features.Products.Commands.EditProducts;
using ResModel = Result;
public class EditProductsCommandHandler : IRequestHandler<EditProductsCommand, ResModel>
{
    private readonly IAppDbContext _appDbContext;
    private readonly IMapper _mapper;
    public EditProductsCommandHandler(IAppDbContext appDbContext, IMapper mapper)
        => (_appDbContext, _mapper) = (appDbContext, mapper);

    public async Task<ResModel> Handle(EditProductsCommand command, CancellationToken cancellationToken)
    {
        try
        {
            var existingProduct = await _appDbContext.Products
                      .Include(c => c.Images)
                      .Include(m => m.Magazine)
                      .FirstOrDefaultAsync(c => c.Id == command.Id);
            if (existingProduct == null)
                return ResModel.Failure(new[] { "Product не найдена" });

            _mapper.Map(command, existingProduct);

            _appDbContext.Products.Update(existingProduct);

            await _appDbContext.SaveChangesAsync(cancellationToken);

            return ResModel.Success();
        }
        catch (Exception ex)
        {
            return ResModel.Failure(new[] { ex.InnerException?.Message ?? ex.Message });
        }
    }
}
