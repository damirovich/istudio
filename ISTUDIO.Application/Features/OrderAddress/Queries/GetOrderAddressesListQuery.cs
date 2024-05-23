using ISTUDIO.Application.Features.OrderAddress.DTOs;

namespace ISTUDIO.Application.Features.OrderAddress.Queries;
using ResModel = OrderAddressListReponseDTO;
public class GetOrderAddressesListQuery : IRequest<ResModel>
{
    public class Handler : IRequestHandler<GetOrderAddressesListQuery, ResModel>
    {
        private readonly IAppDbContext _appDbContext;
        private readonly IMapper _mapper;

        public Handler(IAppDbContext appDbContext, IMapper mapper)
        {
            _appDbContext = appDbContext;
            _mapper = mapper;
        }

        public async Task<ResModel> Handle(GetOrderAddressesListQuery query, CancellationToken cancellationToken)
        {
            var orderAddresses = await _appDbContext.OrderAddresses.ToListAsync(cancellationToken);
            var orderAddressesDto = _mapper.Map<List<OrderAddressDTO>>(orderAddresses);

            return new ResModel { OrderAddresses = orderAddressesDto };
        }
    }
}
