
namespace ISTUDIO.Application.Features.Orders.Queries;
public class GetOrderStatusNameQuery : IRequest<string>
{
    public string InternalStatusCode { get; set; }

    public class Handler : IRequestHandler<GetOrderStatusNameQuery, string>
    {
        private readonly IAppDbContext _appDbContext;

        public Handler(IAppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public async Task<string> Handle(GetOrderStatusNameQuery query, CancellationToken cancellationToken)
        {
            var status = await _appDbContext.OrderStatus
                .FirstOrDefaultAsync(s => s.NameEng == query.InternalStatusCode, cancellationToken);

            return status != null ? status.NameRus : "Неизвестный статус";
        }
    }
}
