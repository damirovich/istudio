using AutoMapper.QueryableExtensions;
using ISTUDIO.Application.Features.ModelsDTO;
using ISTUDIO.Application.Features.Products.DTOs;
using ISTUDIO.Application.Features.ShoppingCarts.DTOs;
using System.ComponentModel.DataAnnotations;

namespace ISTUDIO.Application.Features.ShoppingCarts.Queries;

using ResModel = ShopingResponseDTO;

public class GetShoppingCartsByUserIdQuery : IRequest<ResModel>
{
    [Required]
    public string UserId { get; set; }

    public class Handler : IRequestHandler<GetShoppingCartsByUserIdQuery, ResModel>
    {
        private readonly IAppDbContext _appDbContext;
        private readonly IMapper _mapper;

        public Handler(IAppDbContext appDbContext, IMapper mapper)
        {
            _appDbContext = appDbContext;
            _mapper = mapper;
        }

        public async Task<ResModel> Handle(GetShoppingCartsByUserIdQuery query, CancellationToken cancellationToken)
        {

            var shoppingCarts = await _appDbContext.ShoppingCarts
                .Include(sc => sc.Products)
                    .ThenInclude(p => p.Discount)
                .Include(sc => sc.Products)
                    .ThenInclude(p => p.Images)
                .Include(sc => sc.Products)
                    .ThenInclude(p => p.Magazine)
                .Where(cart => cart.UserId == query.UserId)
                .AsNoTracking()
                .ToListAsync(cancellationToken);

            if (!shoppingCarts.Any())
            {
                return null;
            }

            var groupedCarts = shoppingCarts.GroupBy(cart => cart.UserId).FirstOrDefault();

            if (groupedCarts == null)
            {
                return null;
            }

            var magazineProducts = groupedCarts.SelectMany(cart => cart.Products)
                .GroupBy(p => p.Magazine.Id)
                .Select(g => new ShoppingMagazineDTO
                {
                    Magazine = _mapper.Map<MagazineDTO>(g.First().Magazine),
                    Products = g.Select(p => _mapper.Map<ProductsShoppinDTO>(p)).ToList()
                }).ToList();

            var response = new ResModel
            {
                UserId = groupedCarts.Key,
                TotalAmount = magazineProducts.SelectMany(mp => mp.Products).Sum(p => p.SumProductCart),
                TotalQuantyProduct = magazineProducts.SelectMany(mp => mp.Products).Sum(p => p.QuantyProductCart),
                MagazineProducts = magazineProducts
            };

            return response;
        }

    }
}
