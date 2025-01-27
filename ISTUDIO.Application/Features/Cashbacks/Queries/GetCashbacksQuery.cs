using AutoMapper.QueryableExtensions;
using ISTUDIO.Application.Features.Cashbacks.DTOs;

namespace ISTUDIO.Application.Features.Cashbacks.Queries;

using ResModel = PaginatedList<CashbacksDTO>;
public class GetCashbacksQuery : IRequest<ResModel>
{
    public PaginatedParameters Parameters { get; set; }

    public class Handler : IRequestHandler<GetCashbacksQuery, ResModel>
    {
        private readonly IAppDbContext _appDbContext;
        private readonly IMapper _mapper;

        public Handler(IAppDbContext appDbContext, IMapper mapper)
            => (_appDbContext, _mapper) = (appDbContext, mapper);

        public async Task<ResModel> Handle(GetCashbacksQuery request, CancellationToken cancellationToken)
        {
            var resCashback =  _appDbContext.Cashbacks
              .AsNoTracking()
              .OrderByDescending(c => c.Id)
              .ProjectTo<CashbacksDTO>(_mapper.ConfigurationProvider)
              .PaginatedListAsync(request.Parameters.PageNumber, request.Parameters.PageSize);

            return await resCashback;
        }
    }
}
