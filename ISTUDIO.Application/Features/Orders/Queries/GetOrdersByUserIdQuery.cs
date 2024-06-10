
using ISTUDIO.Application.Features.Orders.DTOs;

namespace ISTUDIO.Application.Features.Orders.Queries;
using ResModel = List<OrderResponseDTO>;
public class GetOrdersByUserIdQuery : IRequest<ResModel>
{
    public string UserId { get; set; }

    public class Handler : IRequestHandler<GetOrdersByUserIdQuery, ResModel>
    {
        private readonly IAppDbContext _appDbContext;
        private readonly IMapper _mapper;
        public Handler (IAppDbContext appDbContext, IMapper mapper)
        {
            _appDbContext = appDbContext;
            _mapper = mapper;
        }

        public async Task<ResModel> Handle(GetOrdersByUserIdQuery query, CancellationToken cancellationToken)
        {
            var orders = await _appDbContext.Orders
                .Include(o => o.Customers)
                .Include(o => o.Products)
                    .ThenInclude(pi => pi.Images)
                .Include(o => o.Products)
                    .ThenInclude(pd => pd.Discount)
                .Where(o => o.UserId == query.UserId)
                .OrderByDescending(o => o.Id)
                .ToListAsync();
            if (orders == null || !orders.Any())
            {
                return new ResModel();
            }
            var responseDto = _mapper.Map<ResModel>(orders);
           
            return responseDto;
        }
    }
}
