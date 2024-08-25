using AutoMapper.QueryableExtensions;
using ISTUDIO.Application.Features.Orders.DTOs;
using System.Globalization;

namespace ISTUDIO.Application.Features.Orders.Queries;
using ResModel = List<OrderResponseDTO>;
public class GetSearchOrdersQuery : IRequest<ResModel>
{
    public PaginationWithSearchParameters Parameters { get; set; }

    public class Handler : IRequestHandler<GetSearchOrdersQuery, ResModel>
    {
        private readonly IAppDbContext _appDbContext;
        private readonly IMapper _mapper;
        public Handler(IAppDbContext appDbContext, IMapper mapper)
        {
            _appDbContext = appDbContext;
            _mapper = mapper;
        }

        public async Task<ResModel> Handle(GetSearchOrdersQuery query, CancellationToken cancellationToken)
        {
            var ordersQuery = _appDbContext.Orders
                .AsNoTracking()
                .OrderByDescending(o => o.Id)
                .ProjectTo<OrderResponseDTO>(_mapper.ConfigurationProvider)
                .ToListAsync();
                // .PaginatedListAsync(query.Parameters.PageNumber, query.Parameters.PageSize);
            
            return await ordersQuery;
        }
    }

}
