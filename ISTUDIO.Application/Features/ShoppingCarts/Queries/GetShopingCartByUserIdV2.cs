using AutoMapper.QueryableExtensions;
using ISTUDIO.Application.Features.ShoppingCarts.DTOs;
using System.ComponentModel.DataAnnotations;

namespace ISTUDIO.Application.Features.ShoppingCarts.Queries;

using ResModel = ShopingResponseDTO;

public class GetShoppingCartsByUserId : IRequest<ResModel>
{
    [Required]
    public string UserId { get; set; }

    public class Handler : IRequestHandler<GetShoppingCartsByUserId, ResModel>
    {
        private readonly IAppDbContext _appDbContext;
        private readonly IMapper _mapper;

        public Handler(IAppDbContext appDbContext, IMapper mapper)
        {
            _appDbContext = appDbContext;
            _mapper = mapper;
        }

        public async Task<ResModel> Handle(GetShoppingCartsByUserId query, CancellationToken cancellationToken)
        {
            // Получаем все корзины покупок для указанного пользователя из базы данных
            var shoppingCarts = await _appDbContext.ShoppingCarts
                .Where(cart => cart.UserId == query.UserId)
                .AsNoTracking()
                .ProjectTo<ShopingResponseDTO>(_mapper.ConfigurationProvider)
                .ToListAsync(cancellationToken);

            // Если не найдено корзин покупок, вернем null или можем выбросить исключение, если необходимо
            if (!shoppingCarts.Any())
            {
                return null;
            }

            // Группируем корзины покупок по UserId (в данном случае это будет одна группа)
            var group = shoppingCarts.GroupBy(cart => cart.UserId).FirstOrDefault();

            if (group == null)
            {
                return null;
            }

            // Вычисляем TotalAmount и TotalQuantyProduct для группы
            var shopingResponseDTO = new ShopingResponseDTO
            {
                UserId = group.Key,
                TotalAmount = group.SelectMany(cart => cart.Products)
                                   .Sum(product => product.Price * product.QuantyProductCart),
                TotalQuantyProduct = group.Sum(cart => cart.QuantyProduct),
                Products = group.SelectMany(cart => cart.Products).ToList()
            };

            // Возвращаем результат в виде объекта ShopingResponseDTO
            return shopingResponseDTO;
        }
    }
}
