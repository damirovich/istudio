
namespace ISTUDIO.Application.Features.FavoriteProducts.Commands;
using ResModel = Result;
public class DeleteFavoriteProductsCommand : IRequest<ResModel>
{
    public int Id { get; set; }

    public class Handler : IRequestHandler<DeleteFavoriteProductsCommand, ResModel>
    {
        private readonly IAppDbContext _appDbContext;

        public Handler(IAppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public async Task<ResModel> Handle(DeleteFavoriteProductsCommand command, CancellationToken cancellationToken)
        {
            try
            {
                var existingFavoriteProducts = await _appDbContext.FavoriteProducts.FindAsync(command.Id);

                if (existingFavoriteProducts == null)
                    return ResModel.Failure(new[] { "FavoriteProducts не найдена" });

                _appDbContext.FavoriteProducts.Remove(existingFavoriteProducts);

                await _appDbContext.SaveChangesAsync(cancellationToken);

                return ResModel.Success();
            }
            catch (Exception ex)
            {
                return ResModel.Failure(new[] { $"Error delete FavoriteProducts {ex.Message}" });
            }
        }
    }
}
