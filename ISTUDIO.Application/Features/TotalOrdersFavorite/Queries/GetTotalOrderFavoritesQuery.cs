using ISTUDIO.Application.Features.TotalOrdersFavorite.DTOs;
namespace ISTUDIO.Application.Features.TotalOrdersFavorite.Queries;

using ResModel = TotalOrderFavoritesResponseDTO;
public class GetTotalOrderFavoritesQuery : IRequest<ResModel>
{
    public string UserId { get; set; }

    public class Handler : IRequestHandler<GetTotalOrderFavoritesQuery, ResModel>
    {
        private readonly IAppDbContext _appDbContext;
        private readonly IMapper _mapper;

        public Handler(IAppDbContext appDbContext, IMapper mapper)
            => (_appDbContext, _mapper) = (appDbContext, mapper);

        public async Task<ResModel> Handle(GetTotalOrderFavoritesQuery query, CancellationToken cancellationToken)
        {
            var totalOrders = await _appDbContext.Orders
                .AsNoTracking()
                .Where(o => o.UserId == query.UserId)
                .CountAsync(cancellationToken);

            var totalFavorites = await _appDbContext.FavoriteProducts
                .AsNoTracking()
                .Where(f => f.UserId == query.UserId)
                .CountAsync(cancellationToken);

            var response = new ResModel
            {
                TotalOrders = totalOrders,
                TotalFavoriteProducts = totalFavorites
            };

            return response;
        }

    }
}
