using AutoMapper.QueryableExtensions;
using ISTUDIO.Application.Features.CashbackTransactions.DTOs;

namespace ISTUDIO.Application.Features.CashbackTransactions.Queries;
using ResModel = PaginatedList<CashTranResDTO>;
public class GetCashTransactionResQuery : IRequest<ResModel>
{
    public PaginatedParameters Parameters { get; set; }

    public class Handler : IRequestHandler<GetCashTransactionResQuery, ResModel>
    {
        private readonly IAppDbContext _appDbContext;
        private readonly IMapper _mapper;

        public Handler(IAppDbContext appDbContext, IMapper mapper)
        {
            _appDbContext = appDbContext;
            _mapper = mapper;
        }

        public async Task<ResModel> Handle(GetCashTransactionResQuery query, CancellationToken cancellationToken)
        {
            var cashTran = _appDbContext.CashbackTransactions
              .AsNoTracking()
              .OrderByDescending(c => c.Id)
              .ProjectTo<CashTranResDTO>(_mapper.ConfigurationProvider)
              .PaginatedListAsync(query.Parameters.PageNumber, query.Parameters.PageSize);

            return await cashTran;
        }
    }
}
