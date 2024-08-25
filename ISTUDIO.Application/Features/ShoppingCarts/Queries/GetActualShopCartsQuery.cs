using ISTUDIO.Application.Features.Products.DTOs;
using ISTUDIO.Application.Features.ShoppingCarts.DTOs;

namespace ISTUDIO.Application.Features.ShoppingCarts.Queries;
using ResModel =  ActualShopCartsResponseDTO;

public class GetActualShopCartsQuery : IRequest<List<ResModel>>
{
    public class Handler : IRequestHandler<GetActualShopCartsQuery, List<ResModel>>
    {
        private readonly IAppDbContext _appDbContext;
        private readonly IMapper _mapper;
        private readonly IAppUserService _appUserService;

        public Handler(IAppDbContext appDbContext, IMapper mapper, IAppUserService appUserService)
        {
            _appDbContext = appDbContext;
            _mapper = mapper;
            _appUserService = appUserService;
        }

        public async Task<List<ResModel>> Handle(GetActualShopCartsQuery query, CancellationToken cancellationToken)
        {
            var shoppingCarts = await _appDbContext.ShoppingCarts
                .Include(sc => sc.Products)
                    .ThenInclude(p => p.Discount)
                .Include(sc => sc.Products) 
                    .ThenInclude(p => p.Images)
                .AsNoTracking()
                .ToListAsync(cancellationToken);

            if (!shoppingCarts.Any())
            {
                return new List<ResModel>(); // Возвращаем пустой список вместо одного объекта
            }

            var responseList = new List<ResModel>();

            foreach (var groupedCarts in shoppingCarts.GroupBy(cart => cart.UserId))
            {
                var products = groupedCarts.SelectMany(cart => cart.Products)
                    .GroupBy(p => new { p.Id, p.Name, p.Model, p.Price })
                    .Select(g => new ProductsShoppinDTO
                    {
                        CartId = g.FirstOrDefault()?.ShoppingCarts.FirstOrDefault()?.Id ?? 0,
                        Id = g.Key.Id,
                        Name = g.Key.Name,
                        Model = g.Key.Model,
                        Price = g.Key.Price,
                        QuantyProductCart = g.Sum(p => p.ShoppingCarts.FirstOrDefault()?.QuantyProduct ?? 0),
                        SumProductCart = g.Key.Price * g.Sum(p => p.ShoppingCarts.FirstOrDefault()?.QuantyProduct ?? 0),
                        Images = _mapper.Map<ICollection<ProductImagesDTO>>(g.FirstOrDefault()?.Images),
                        ProductDiscount = _mapper.Map<ProductDiscountDTO>(g.FirstOrDefault()?.Discount)
                    }).ToList();

                var user = await _appUserService.GetUserDetailsByUserIdAsync(groupedCarts.Key);

                var resModel = new ResModel
                {
                    UserPhoneNumber = user.UserPhoneNumber ?? "",
                    TotalAmount = products.Sum(p => p.SumProductCart),
                    TotalQuantyProduct = products.Sum(p => p.QuantyProductCart),
                    Products = products
                };

                responseList.Add(resModel);
            }

            return responseList;
        }


    }
}
