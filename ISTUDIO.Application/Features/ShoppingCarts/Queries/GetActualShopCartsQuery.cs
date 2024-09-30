using ISTUDIO.Application.Features.Products.DTOs;
using ISTUDIO.Application.Features.ShoppingCarts.DTOs;

namespace ISTUDIO.Application.Features.ShoppingCarts.Queries;
using ResModel = PaginatedList<ActualShopCartsResponseDTO>;

public class GetActualShopCartsQuery : IRequest<ResModel>
{
    public PaginatedParameters Parameters { get; set; }

    public class Handler : IRequestHandler<GetActualShopCartsQuery, ResModel>
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

        public async Task<ResModel> Handle(GetActualShopCartsQuery query, CancellationToken cancellationToken)
        {
            // Получение всех корзин с продуктами
            var shoppingCartsQuery = _appDbContext.ShoppingCarts
                .Include(sc => sc.Products)
                    .ThenInclude(p => p.Discount)
                .Include(sc => sc.Products)
                    .ThenInclude(p => p.Images)
                .AsNoTracking()
                .OrderBy(sc => sc.Id); // Сортировка по ID или другому критерию

            // Применяем пагинацию
            var paginatedCarts = await shoppingCartsQuery
                .PaginatedListAsync(query.Parameters.PageNumber, query.Parameters.PageSize);

            if (!paginatedCarts.Items.Any())
            {
                return new ResModel( new List<ActualShopCartsResponseDTO>().AsReadOnly(), 0, 0, 0);

            }

            // Формирование списка для возврата
            var responseList = new List<ActualShopCartsResponseDTO>();

            foreach (var groupedCarts in paginatedCarts.Items.GroupBy(cart => cart.UserId))
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

                var resModel = new ActualShopCartsResponseDTO
                {
                    UserPhoneNumber = user.UserPhoneNumber ?? "",
                    TotalAmount = products.Sum(p => p.SumProductCart),
                    TotalQuantyProduct = products.Sum(p => p.QuantyProductCart),
                    Products = products
                };

                responseList.Add(resModel);
            }

            return new ResModel(responseList, paginatedCarts.TotalCount, query.Parameters.PageNumber, query.Parameters.PageSize);
        }


    }
}
