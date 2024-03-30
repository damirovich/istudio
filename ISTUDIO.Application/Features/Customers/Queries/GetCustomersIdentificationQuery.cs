using ISTUDIO.Application.Features.Customers.DTOs;

namespace ISTUDIO.Application.Features.Customers.Queries;

using ResModel = CustomerResponseDTO;
public class GetCustomersIdentificationQuery : IRequest<ResModel>
{
    public string UserId { get; set; }

    public class Handler : IRequestHandler<GetCustomersIdentificationQuery, ResModel>
    {
        private readonly IAppDbContext _appDbContext;
        private readonly IMapper _mapper;

        public Handler(IAppDbContext appDbContext, IMapper mapper)
            => (_appDbContext, _mapper) = (appDbContext, mapper);

        public async Task<ResModel> Handle(GetCustomersIdentificationQuery query, CancellationToken cancellationToken)
        {
            // Получаем информацию о клиенте из базы данных
            var customer = await _appDbContext.Customers
                .FirstOrDefaultAsync(c => c.UserId == query.UserId, cancellationToken);

            // Если клиент не найден, или поле Identification равно false, возвращаем информацию о том, что клиент еще не прошел идентификацию
            if (customer == null)
                throw new NotFoundException("Клиент не найден и еще не прошел идентификацию!");

            if (customer.Identification==null || customer.Identification == false)
                throw new NotFoundException("Клиент еще не прошел идентификацию!");

            var responseDto = _mapper.Map<ResModel>(customer);

            return responseDto;
        }
    }
}
