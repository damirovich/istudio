using AutoMapper.QueryableExtensions;
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

            var products = groupedCarts.SelectMany(cart => cart.Products)
                .GroupBy(p => new { p.Id, p.Name, p.Model, p.Price, p.QuantityInStock })
                .Select(g => new ProductsShoppinDTO
                {
                    CartId = g.FirstOrDefault().ShoppingCarts.FirstOrDefault()?.Id ?? 0,
                    Id = g.Key.Id,
                    Name = g.Key.Name,
                    Model = g.Key.Model,
                    Price = g.Key.Price,
                    QuantyProductStock = g.Key.QuantityInStock,
                    QuantyProductCart = g.Sum(p => p.ShoppingCarts.FirstOrDefault()?.QuantyProduct ?? 0),
                    SumProductCart = g.Key.Price * g.Sum(p => p.ShoppingCarts.FirstOrDefault()?.QuantyProduct ?? 0),
                    Images = _mapper.Map<ICollection<ProductImagesDTO>>(g.FirstOrDefault()?.Images),
                    ProductDiscount = _mapper.Map<ProductDiscountDTO>(g.FirstOrDefault()?.Discount)
                }).ToList();

            var response = new ResModel
            {
                UserId = groupedCarts.Key,
                TotalAmount = products.Sum(p => p.SumProductCart),
                TotalQuantyProduct = products.Sum(p => p.QuantyProductCart),
                Products = products
            };

            return response;
        }
    }
}
