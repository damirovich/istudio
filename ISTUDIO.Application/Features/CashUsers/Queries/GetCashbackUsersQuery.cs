
using AutoMapper.QueryableExtensions;
using ISTUDIO.Application.Features.CashUsers.DTOs;

namespace ISTUDIO.Application.Features.CashUsers.Queries;
using ResModel = PaginatedList<CashUsersResDTO>;
public class GetCashbackUsersQuery : IRequest<ResModel>
{
    public PaginatedParameters Parameters { get; set; }

    public class Handler : IRequestHandler<GetCashbackUsersQuery, ResModel>
    {
        private readonly IAppDbContext _appDbContext;
        private readonly IMapper _mapper;

        public Handler(IAppDbContext appDbContext, IMapper mapper)
            => (_appDbContext, _mapper) = (appDbContext, mapper);

        public async Task<ResModel> Handle(GetCashbackUsersQuery query, CancellationToken cancellationToken)
        {
            var userCashback = _appDbContext.UserCashbacks
             .AsNoTracking()
             .OrderByDescending(c => c.Id)
             .ProjectTo<CashUsersResDTO>(_mapper.ConfigurationProvider)
             .PaginatedListAsync(query.Parameters.PageNumber, query.Parameters.PageSize);

            return await userCashback;
        }
    }
}
