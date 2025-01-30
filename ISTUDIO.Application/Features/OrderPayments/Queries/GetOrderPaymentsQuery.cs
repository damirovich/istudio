using AutoMapper.QueryableExtensions;
using ISTUDIO.Application.Features.OrderPayments.DTOs;

namespace ISTUDIO.Application.Features.OrderPayments.Queries;
using ResModel = PaginatedList<OrderPaymentResDTO>;
public class GetOrderPaymentsQuery : IRequest<ResModel>
{
    public PaginatedParameters Parameters { get; set; }

    public class Handler : IRequestHandler<GetOrderPaymentsQuery, ResModel>
    {
        private readonly IAppDbContext _appDbContext;
        private readonly IMapper _mapper;

        public Handler(IAppDbContext appDbContext, IMapper mapper)
            => (_appDbContext, _mapper) = (appDbContext, mapper);

        public async Task<ResModel> Handle(GetOrderPaymentsQuery query, CancellationToken cancellationToken)
        {
            var orderPayment = await _appDbContext.OrderPayments
               .ProjectTo<OrderPaymentResDTO>(_mapper.ConfigurationProvider)
               .PaginatedListAsync(query.Parameters.PageNumber, query.Parameters.PageSize);

            return orderPayment;
        }
    }
}
