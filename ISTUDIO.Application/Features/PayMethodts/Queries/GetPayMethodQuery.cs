using ISTUDIO.Application.Features.PayMethodts.DTOs;

namespace ISTUDIO.Application.Features.PayMethodts.Queries;
using ResModel = List<PayMethodDTO>;
public class GetPayMethodQuery : IRequest<ResModel>
{
    public class Handler : IRequestHandler<GetPayMethodQuery, ResModel>
    { 
        private readonly IAppDbContext _appDbContext;
        private readonly IMapper _mapper;

        public Handler(IAppDbContext appDbContext, IMapper mapper)
            => (_appDbContext, _mapper) = (appDbContext, mapper);

        public async Task<ResModel> Handle(GetPayMethodQuery query, CancellationToken cancellationToken)
        {
            try
            {
                var res = await _appDbContext.PaymentMethods
                    .Include(pm => pm.PaymentType)
                    .ToListAsync(cancellationToken);

                //var res = await _appDbContext.PaymentMethods.ToListAsync(cancellationToken);

                return _mapper.Map<ResModel>(res);
            }
            catch (Exception ex)
            {
                // Логирование ошибки или другая обработка
                throw new ApplicationException("Произошла ошибка при получении payment methods.", ex);
            }
        }
    }
}
