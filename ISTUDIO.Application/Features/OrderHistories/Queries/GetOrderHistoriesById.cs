using AutoMapper.QueryableExtensions;
using ISTUDIO.Application.Features.OrderHistories.DTOs;
using ISTUDIO.Application.Common.Exceptions;

namespace ISTUDIO.Application.Features.OrderHistories.Queries;
using ResModel = OrderHistoriesListResponseDTO;
public class GetOrderHistoriesById : IRequest<ResModel>
{
    public int Id { get; set; }

    public class Handler : IRequestHandler<GetOrderHistoriesById, ResModel>
    {
        private readonly IAppDbContext _appDbContext;
        private readonly IMapper _mapper;
        public Handler(IAppDbContext appDbContext, IMapper mapper) =>
            (_appDbContext, _mapper )= (appDbContext, mapper);

        public async Task<ResModel> Handle(GetOrderHistoriesById query, CancellationToken cancellationToken)
        {
            var orderHistoriesStatus = await _appDbContext.OrderStatusHistories
                .Where(s=>s.OrderId == query.Id)
                .OrderByDescending(c => c.Id)
                .ProjectTo<OrderHistoriesDTO>(_mapper.ConfigurationProvider)
                .ToListAsync(cancellationToken);

            if (orderHistoriesStatus == null )
            {
                throw new NotFoundException("Статус заказ не найден");
            }

            return new ResModel { OrderHistories = orderHistoriesStatus };
            
        }
    }
}
