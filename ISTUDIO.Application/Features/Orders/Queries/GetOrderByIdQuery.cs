using ISTUDIO.Application.Features.Orders.DTOs;

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
            var orders = await _appDbContext.Orders
                .Include(o => o.Customers)
                .Where(o => o.Id == query.OrderId)
                .FirstOrDefaultAsync(cancellationToken);
            if (orders == null)
            {
                return new ResModel();
            }
            var responseDto = _mapper.Map<ResModel>(orders);

            return responseDto;
        }
    }
}

