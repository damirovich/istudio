
using AutoMapper.QueryableExtensions;
using ISTUDIO.Application.Features.CashUsers.DTOs;

namespace ISTUDIO.Application.Features.CashUsers.Queries;

using ResModel = CashUsersResDTO;
public class GetUserCashbackByUserIdQuery : IRequest<ResModel>
{
    public string UserId { get; set; }

    public class Handler : IRequestHandler<GetUserCashbackByUserIdQuery, ResModel>
    {
        private readonly IAppDbContext _appDbContext;
        private readonly IMapper _mapper;

        public Handler(IAppDbContext appDbContext, IMapper mapper)
            => (_appDbContext, _mapper) = (appDbContext, mapper);

        public async Task<ResModel> Handle(GetUserCashbackByUserIdQuery request, CancellationToken cancellationToken)
        {
            var userCashback = _appDbContext.UserCashbacks
            .AsNoTracking()
            .OrderByDescending(c => c.Id)
            .ProjectTo<ResModel>(_mapper.ConfigurationProvider)
            .FirstOrDefaultAsync(cancellationToken);

            if (userCashback == null)
                throw new NotFoundException($"Cashback User with ID {request.UserId} not found.");

            return await userCashback;
        }
    }
}
