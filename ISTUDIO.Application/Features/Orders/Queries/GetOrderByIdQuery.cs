using ISTUDIO.Application.Features.Orders.DTOs;
using ISTUDIO.Application.Features.Products.DTOs;

namespace ISTUDIO.Application.Features.Orders.Queries;
using ResModel = OrderResponseDTO;

public class GetOrderByIdQuery : IRequest<ResModel>
{
    public int OrderId { get; set; }

    public class Handler : IRequestHandler<GetOrderByIdQuery, ResModel>
    {
        private readonly IAppDbContext _appDbContext;
        private readonly IMapper _mapper;
        public Handler(IAppDbContext appDbContext, IMapper mapper)
        {
            _appDbContext = appDbContext;
            _mapper = mapper;
        }

        public async Task<ResModel> Handle(GetOrderByIdQuery query, CancellationToken cancellationToken)
        {
            var order = await _appDbContext.Orders
                //.Include(o => o.Customers) // Включаем клиентов
                .Include(o => o.Details)   // Включаем детали заказа
                    .ThenInclude(d => d.Magazines)  // Включаем магазины через детали заказа
                .Include(o => o.Details)
                    .ThenInclude(d => d.Product)   // Включаем продукты через детали заказа
                .Include(o => o.Products)
                    .ThenInclude(o => o.Images)
                .Where(o => o.Id == query.OrderId)
                .FirstOrDefaultAsync(cancellationToken);

            if (order == null)
            {
                return new ResModel(); // Если заказ не найден, возвращаем пустой DTO
            }

            // Маппим заказ в DTO
            var responseDto = _mapper.Map<ResModel>(order);

            return responseDto;
        }
    }
}
