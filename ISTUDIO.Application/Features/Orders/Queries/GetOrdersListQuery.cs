using AutoMapper.QueryableExtensions;
using ISTUDIO.Application.Features.Orders.DTOs;
using System.ComponentModel.DataAnnotations;

namespace ISTUDIO.Application.Features.Orders.Queries;
using ResModel = PaginatedList<OrderResponseDTO>;

public class GetOrdersListQuery : IRequest<ResModel>
{
    [Required]
    public PaginatedParameters Parameters { get; set; }

    public class Handler : IRequestHandler<GetOrdersListQuery, ResModel>
    {
        private readonly IAppDbContext _appDbContext;
        private readonly IMapper _mapper;
        public Handler(IAppDbContext appDbContext, IMapper mapper)
        {
            _appDbContext = appDbContext;
            _mapper = mapper;
        }

        public async Task<ResModel> Handle(GetOrdersListQuery query, CancellationToken cancellationToken)
        {
            var orders = await _appDbContext.Orders
                .Include(o => o.Customers) // Включаем информацию о клиентах
                .Include(o => o.Details)   // Включаем детали заказа
                    .ThenInclude(d => d.Magazines) // Включаем магазины через детали заказа
                .Include(o => o.Details)
                    .ThenInclude(d => d.Product)  // Включаем продукты через детали заказа
                    .ThenInclude(pi => pi.Images)
                .OrderByDescending(o => o.Id)
                .ProjectTo<OrderResponseDTO>(_mapper.ConfigurationProvider)
                .PaginatedListAsync(query.Parameters.PageNumber, query.Parameters.PageSize);

            return orders;
        }
    }
}
