using ISTUDIO.Application.Features.Orders.DTOs;
using Microsoft.EntityFrameworkCore;

namespace ISTUDIO.Application.Features.Orders.Queries;
using ResModel = List<OrderResponseDTO>;

public class GetOrdersByUserIdQuery : IRequest<ResModel>
{
    public string UserId { get; set; }

    public class Handler : IRequestHandler<GetOrdersByUserIdQuery, ResModel>
    {
        private readonly IAppDbContext _appDbContext;
        private readonly IMapper _mapper;

        public Handler(IAppDbContext appDbContext, IMapper mapper)
        {
            _appDbContext = appDbContext;
            _mapper = mapper;
        }

        public async Task<ResModel> Handle(GetOrdersByUserIdQuery query, CancellationToken cancellationToken)
        {
            try
            {
                
                var orders = await _appDbContext.Orders
                    .Include(o => o.Details)
                        .ThenInclude(d => d.Product)
                            .ThenInclude(pd => pd.Discount)
                    .Include(o => o.Details)
                        .ThenInclude(d => d.Magazines) // Убедитесь, что `Magazine` правильно называется
                    .Include(o => o.Products)
                        .ThenInclude(p => p.Images)
                    .Include(s => s.Status)
                    .Where(o => o.UserId != null && o.UserId == query.UserId) // Проверка UserId
                    .OrderByDescending(o => o.Id)
                    .ToListAsync(cancellationToken);


                if (orders == null || !orders.Any())
                {
                    return new ResModel(); // Возвращаем пустой список, если заказы не найдены
                }

                // Маппим найденные заказы в DTO
                var responseDto = _mapper.Map<ResModel>(orders);

                return responseDto;

            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
