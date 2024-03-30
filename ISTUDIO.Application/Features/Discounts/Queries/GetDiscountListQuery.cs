
using AutoMapper.QueryableExtensions;
using ISTUDIO.Application.Features.Discounts.DTOs;
using ISTUDIO.Application.Features.Products.DTOs;

namespace ISTUDIO.Application.Features.Discounts.Queries;
using ResModel = PaginatedList<DiscountResponseListDTO>;
public class GetDiscountListQuery : IRequest<ResModel>
{
    public PaginatedParameters Parameters { get; set; }

    public class Handler : IRequestHandler<GetDiscountListQuery, ResModel>
    {
        private readonly IAppDbContext _appDbContext;
        private readonly IMapper _mapper;

        public Handler(IAppDbContext appDbContext, IMapper mapper)
        {
            _appDbContext = appDbContext;
            _mapper = mapper;
        }

        public async Task<ResModel> Handle(GetDiscountListQuery query, CancellationToken cancellationToken)
        {
            var discounts = _appDbContext.Discounts
              .AsNoTracking()
              .OrderByDescending(c => c.Id)
              .ProjectTo<DiscountResponseListDTO>(_mapper.ConfigurationProvider)
              .PaginatedListAsync(query.Parameters.PageNumber, query.Parameters.PageSize);

            return await discounts;
        }
    }
}
