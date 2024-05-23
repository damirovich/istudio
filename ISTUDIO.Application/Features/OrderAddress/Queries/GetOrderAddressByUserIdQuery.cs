
using ISTUDIO.Application.Features.OrderAddress.DTOs;

namespace ISTUDIO.Application.Features.OrderAddress.Queries;
using ResModel = OrderAddressDTO;
public class GetOrderAddressByUserIdQuery : IRequest<ResModel>
{
    public string UserId { get; set; }

    public class Handler : IRequestHandler<GetOrderAddressByUserIdQuery, ResModel>
    {
        private readonly IAppDbContext _appDbContext;
        private readonly IMapper _mapper;

        public Handler(IAppDbContext appDbContext, IMapper mapper)
        {
            _appDbContext = appDbContext;
            _mapper = mapper;
        }

        public async Task<ResModel> Handle(GetOrderAddressByUserIdQuery query, CancellationToken cancellationToken)
        {
            var orderAddresses = await _appDbContext.OrderAddresses
                .Where(x => x.UserId == query.UserId && x.OrderId == null)
                .ToListAsync(cancellationToken);

            return _mapper.Map<ResModel>(orderAddresses);
        }
    }
}
