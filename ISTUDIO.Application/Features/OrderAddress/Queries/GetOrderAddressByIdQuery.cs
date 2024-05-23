using ISTUDIO.Application.Features.OrderAddress.DTOs;
using ISTUDIO.Domain.EntityModel;

namespace ISTUDIO.Application.Features.OrderAddress.Queries;
using ResModel = OrderAddressDTO;
public class GetOrderAddressByIdQuery : IRequest<ResModel>
{
    public int Id { get; set; }

    public class Handler : IRequestHandler<GetOrderAddressByIdQuery, ResModel>
    {
        private readonly IAppDbContext _appDbContext;
        private readonly IMapper _mapper;

        public Handler(IAppDbContext appDbContext, IMapper mapper)
        {
            _appDbContext = appDbContext;
            _mapper = mapper;
        }

        public async Task<ResModel> Handle(GetOrderAddressByIdQuery query, CancellationToken cancellationToken)
        {
            var orderAddress = await _appDbContext.OrderAddresses
                .FirstOrDefaultAsync(x => x.Id == query.Id, cancellationToken);

            if (orderAddress == null)
            {
                throw new NotFoundException(nameof(OrderAddressEntity), query.Id);
            }

            return _mapper.Map<ResModel>(orderAddress);
        }
    }
}
