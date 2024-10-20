using ISTUDIO.Application.Features.Magazines.DTOs;

namespace ISTUDIO.Application.Features.Magazines.Queries;
using ResModel = MagazinesDTO;
public class GetMagazineByIdQuery : IRequest<ResModel>
{
    public int MagazineId { get; set; }
    public class Handler : IRequestHandler<GetMagazineByIdQuery, ResModel>
    {
        private readonly IAppDbContext _appDbContext;
        private readonly IMapper _mapper;

        public Handler(IAppDbContext appDbContext, IMapper mapper)
        {
            _appDbContext = appDbContext;
            _mapper = mapper;
        }

        public async Task<ResModel> Handle(GetMagazineByIdQuery query, CancellationToken cancellationToken)
        {
            // Поиск магазина по его ID
            var magazine = await _appDbContext.Magazines
                .AsNoTracking()
                .FirstOrDefaultAsync(m => m.Id == query.MagazineId && m.IsActive == true, cancellationToken);

            // Проверка, найден ли магазин
            if (magazine == null)
            {
                return null;
            }

            // Преобразование данных магазина в DTO
            var magazineDto = _mapper.Map<ResModel>(magazine);

            return magazineDto;
        }
    }
}
