using ISTUDIO.Application.Features.UpAppInfo.DTOs;

namespace ISTUDIO.Application.Features.UpAppInfo.Queries;
using ResModel = InfoUpAppDTO;
public class GetInfoAppQuery : IRequest<ResModel>
{
    public string Platform { get; set; } // Платформа, переданная клиентом
    public class Handler : IRequestHandler<GetInfoAppQuery, ResModel>
    {
        private readonly IAppDbContext _appDbContext;
        private readonly IMapper _mapper;

        public Handler(IAppDbContext appDbContext, IMapper mapper)
        {
            _appDbContext = appDbContext;
            _mapper = mapper;
        }
        public async Task<ResModel> Handle(GetInfoAppQuery query, CancellationToken cancellationToken)
        {

            // Проверка, указана ли платформа
            if (string.IsNullOrEmpty(query.Platform))
            {
                throw new ArgumentException("Платформа должна быть указана (iOS или Android).");
            }

            // Получение последней записи для указанной платформы
            var appUpdateInfo = await _appDbContext.AppUpdateInfos
                .Where(x => x.Platform == query.Platform) // Фильтрация по платформе
                .OrderByDescending(x => x.Id)             // Сортировка по Id
                .FirstOrDefaultAsync(cancellationToken);  // Получение последней записи

            // Проверяем, если запись не найдена, возвращаем null
            if (appUpdateInfo == null)
            {
                return null; // Либо можно выбросить исключение, если это критично
            }

            // Маппинг данных на DTO
            var appUpdateInfoDto = _mapper.Map<ResModel>(appUpdateInfo);

            return appUpdateInfoDto;
        }

    }
}
