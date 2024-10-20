using AutoMapper.QueryableExtensions;
using ISTUDIO.Application.Features.Orders.DTOs;
using Microsoft.EntityFrameworkCore;

namespace ISTUDIO.Application.Features.Orders.Queries;
using ResModel = List<OrderResponseDTO>;

public class GetSearchOrdersQuery : IRequest<ResModel>
{
    public PaginationWithSearchParameters Parameters { get; set; }

    public class Handler : IRequestHandler<GetSearchOrdersQuery, ResModel>
    {
        private readonly IAppDbContext _appDbContext;
        private readonly IMapper _mapper;

        public Handler(IAppDbContext appDbContext, IMapper mapper)
        {
            _appDbContext = appDbContext;
            _mapper = mapper;
        }

        public async Task<ResModel> Handle(GetSearchOrdersQuery query, CancellationToken cancellationToken)
        {
            // Формируем запрос с учетом поиска
            var ordersQuery = _appDbContext.Orders
                .Include(o => o.Details)   // Включаем детали заказа
                    .ThenInclude(d => d.Magazines)  // Включаем магазины через детали заказа
                .Include(o => o.Details)
                    .ThenInclude(d => d.Product)   // Включаем продукты через детали заказа
                    .ThenInclude(pi => pi.Images)
                .AsNoTracking()
                .OrderByDescending(o => o.Id)
                .Where(o => string.IsNullOrEmpty(query.Parameters.SearchTerm) ||
                            o.Customers.Any(c => c.FullName.Contains(query.Parameters.SearchTerm) ||
                            o.ShippingAddress.Contains(query.Parameters.SearchTerm)));  // Пример фильтра по имени клиента и адресу доставки

            // Применение пагинации
            var paginatedOrders = await ordersQuery
                .ProjectTo<OrderResponseDTO>(_mapper.ConfigurationProvider)
                .Skip((query.Parameters.PageNumber - 1) * query.Parameters.PageSize)
                .Take(query.Parameters.PageSize)
                .ToListAsync(cancellationToken);

            return paginatedOrders;
        }
    }
}
