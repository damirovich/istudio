using ISTUDIO.Application.Features.OrderPayments.DTOs;

namespace ISTUDIO.Application.Features.OrderPayments.Queries;
using ResModel = OrderPaymentResDTO;
public class GetOrderPaymentsByIdQuery : IRequest<ResModel>
{
    public int OrderPayId { get; set; }

    public class Handler : IRequestHandler<GetOrderPaymentsByIdQuery, ResModel>
    {
        private readonly IAppDbContext _appDbContext;
        private readonly IMapper _mapper;

        public Handler(IAppDbContext appDbContext, IMapper mapper)
            => (_appDbContext, _mapper) = (appDbContext, mapper);

        public async Task<ResModel> Handle(GetOrderPaymentsByIdQuery query, CancellationToken cancellationToken)
        {
            var orderPayment = await _appDbContext.OrderPayments
                .Where(o => o.Id == query.OrderPayId)
                .FirstOrDefaultAsync(cancellationToken);

            if (orderPayment == null)
            {
                return new ResModel(); 
            }

            // Маппим заказ в DTO
            var responseDto = _mapper.Map<ResModel>(orderPayment);

            return responseDto;
        }
    }
}
