

using AutoMapper.QueryableExtensions;
using ISTUDIO.Application.Features.ShoppingCarts.DTOs;
using System.ComponentModel.DataAnnotations;

namespace ISTUDIO.Application.Features.ShoppingCarts.Queries;

using ResModel = List<ShopingResponseDTO>;
public class GetShoppingCartsByUserId  : IRequest<ResModel>
{
    [Required]
    public string UserId { get; set; }

    public class Handler : IRequestHandler<GetShoppingCartsByUserId, ResModel>
    {
        private readonly IAppDbContext _appDbContext;
        private readonly IMapper _mapper;
        public Handler(IAppDbContext appDbContext, IMapper mapper) =>
            (_appDbContext, _mapper) = (appDbContext, mapper);

        public async Task<ResModel> Handle(GetShoppingCartsByUserId query, CancellationToken cancellationToken)
        {

            // Получаем все корзины покупок из базы данных
            var shoppingCarts = await _appDbContext.ShoppingCarts
               .AsNoTracking()
               .ProjectTo<ShopingResponseDTO>(_mapper.ConfigurationProvider)
               .ToListAsync();

            // Группируем корзины покупок по идентификатору пользователя
            var groupedShoppingCarts = shoppingCarts.GroupBy(cart => new { cart.UserId })
               .Select(group => new ShopingResponseDTO
               {
                   // Устанавливаем идентификатор пользователя для группы
                   UserId = group.Key.UserId,

                   // Вычисляем общую сумму покупок в корзине путем суммирования цен всех продуктов
                   TotalAmount = group.SelectMany(cart => cart.Products)
                                     .Sum(product => product.Price * product.QuantyProductCart),

                   // Вычисляем общее количество товаров в корзине путем суммирования количества каждого продукта
                   TotalQuantyProduct = group.Sum(cart => cart.QuantyProduct),

                   // Формируем список уникальных продуктов в корзине
                   Products = group.SelectMany(cart => cart.Products).ToList()
               }).ToList();

            // Возвращаем список сгруппированных корзин покупок
            return groupedShoppingCarts;
        }
    }
}
