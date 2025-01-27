using AutoMapper.QueryableExtensions;
using ISTUDIO.Application.Features.CashbackTransactions.DTOs;

namespace ISTUDIO.Application.Features.CashbackTransactions.Queries;

using ResModel = CashTranResDTO;
public class GetCashbackTranByIdQuery : IRequest<ResModel>
{
    public int CashTranId { get; set; }

    public class Handler : IRequestHandler<GetCashbackTranByIdQuery, ResModel>
    {
        private readonly IAppDbContext _appDbContext;
        private readonly IMapper _mapper;

        public Handler(IAppDbContext appDbContext, IMapper mapper)
            => (_appDbContext, _mapper) = (appDbContext, mapper);

        public async Task<ResModel> Handle(GetCashbackTranByIdQuery request, CancellationToken cancellationToken)
        {
            var cashTran = await _appDbContext.CashbackTransactions
                .AsNoTracking()
                .Where(c => c.Id == request.CashTranId)
                .ProjectTo<ResModel>(_mapper.ConfigurationProvider)
                .FirstOrDefaultAsync(cancellationToken); 

            if (cashTran == null)
                throw new NotFoundException($"Transaction with ID {request.CashTranId} not found.");

            return cashTran;
        }
    }
}
