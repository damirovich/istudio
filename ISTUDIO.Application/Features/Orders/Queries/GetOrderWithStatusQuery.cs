using AutoMapper.QueryableExtensions;
using ISTUDIO.Application.Features.Orders.DTOs;

namespace ISTUDIO.Application.Features.Orders.Queries;
using ResModel = List<GetOrderWithStatusDTO>;
public class GetOrderWithStatusQuery : IRequest<ResModel>
{
    public string OrderStatus { get; set; }

    public class Handler : IRequestHandler<GetOrderWithStatusQuery, ResModel>
    {
        private readonly IAppDbContext _appDbContext;
        private readonly IMapper _mapper;

        public Handler(IAppDbContext appDbContext, IMapper mapper)
            => (_appDbContext, _mapper) = (appDbContext, mapper);

        public async Task<ResModel> Handle(GetOrderWithStatusQuery request, CancellationToken cancellationToken)
        {
            var orderStatus = await _appDbContext.Orders
                  .Include(o => o.Status)
                  .Include(o => o.Payments).ThenInclude(p => p.PaymentMethod)
                  .Where(o => o.Payments.Any(p => p.Status == request.OrderStatus))
                  .OrderByDescending(o => o.Id)
                  .ProjectTo<GetOrderWithStatusDTO>(_mapper.ConfigurationProvider)
                  .ToListAsync(cancellationToken);

            // Для каждого заказа нужно подтянуть CreateTranId из BakaiConfirmTranResponse
            foreach (var order in orderStatus)
            {
                var bakaiConfirm = await _appDbContext.BakaiConfirmTranResponse
                    .Where(x => x.OrderId == order.OrderId.ToString())
                    .FirstOrDefaultAsync(cancellationToken);

                if (bakaiConfirm != null)
                {
                    order.CreateTranId = bakaiConfirm.CreateTranId; 
                }
            }


            return orderStatus;
        }
        
    }
}
