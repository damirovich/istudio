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
                .Include(o => o.Product)
                     .ThenInclude(pi => pi.Images)
                .Include(o => o.Product)
                     .ThenInclude(pd => pd.Discount)
                .Where(s => s.OrderId == query.OrderId)
                .ToListAsync(cancellationToken);

            if (orderDetails == null && orderDetails.Count == 0)
                throw new NotFoundException("Заказ не найден");

            var responseDto = _mapper.Map<ResModel>(orderDetails);

            return responseDto;
        }
    }
}
