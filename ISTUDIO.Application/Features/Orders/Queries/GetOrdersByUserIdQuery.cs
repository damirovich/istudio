
using ISTUDIO.Application.Features.Orders.DTOs;

namespace ISTUDIO.Application.Features.Orders.Queries;
using ResModel = OrderResponseDTO;
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
            var order = await _appDbContext.Orders
                .Include(o=>o.Customers)
                .FirstOrDefaultAsync(o => o.UserId == query.UserId);

            var responseDto = _mapper.Map<ResModel>(order);
           
            return responseDto;
        }
    }
}
