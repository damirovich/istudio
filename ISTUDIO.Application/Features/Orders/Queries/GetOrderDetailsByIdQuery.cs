using ISTUDIO.Application.Features.Orders.DTOs;

namespace ISTUDIO.Application.Features.Orders.Queries;
using ResModel = List<OrderDetailsResponseDTO>;

public class GetOrderDetailsByIdQuery : IRequest<ResModel>
{
    public int OrderId { get; set; }

    public class Handler : IRequestHandler<GetOrderDetailsByIdQuery, ResModel>
    {
        private readonly IAppDbContext _appDbContext;
        private readonly IMapper _mapper;

        public Handler(IAppDbContext appDbContext, IMapper mapper)
        {
            _appDbContext = appDbContext;
            _mapper = mapper;
        }

        public async Task<ResModel> Handle(GetOrderDetailsByIdQuery query, CancellationToken cancellationToken)
        {
            var orderDetails = await _appDbContext.OrderDetails
                .Include(o => o.Product) // Включаем продукт
                     .ThenInclude(pi => pi.Images) // Включаем изображения продукта
                .Include(o => o.Product)
                     .ThenInclude(pd => pd.Discount) // Включаем скидки на продукт
                .Include(o => o.Product)
                     .ThenInclude(o => o.Images)
                .Include(o => o.Magazine) // Включаем магазин через детали заказа
                .Where(s => s.OrderId == query.OrderId) // Фильтр по OrderId
                .ToListAsync(cancellationToken);

            if (!orderDetails.Any())
                throw new NotFoundException("Заказ не найден");

            var responseDto = _mapper.Map<ResModel>(orderDetails);

            return responseDto;
        }
    }
}
