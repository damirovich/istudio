using AutoMapper.QueryableExtensions;
using ISTUDIO.Application.Features.Customers.DTOs;
using System.ComponentModel.DataAnnotations;

namespace ISTUDIO.Application.Features.Customers.Queries;
using ResModel = PaginatedList<GroupedCustomerImagesResponseDTO>;
public class GetCustomerIdentListQuery : IRequest<ResModel>
{
    [Required]
    public PaginatedParameters Parameters { get; set; }
    public class Handler : IRequestHandler<GetCustomerIdentListQuery, ResModel>
    {
        private readonly IAppDbContext _appDbContext;
        private readonly IMapper _mapper;
        private readonly IAppUserService _appUserService;
        public Handler(IAppDbContext appDbContext, IMapper mapper, IAppUserService appUserService) =>
            (_appDbContext, _mapper, _appUserService) = (appDbContext, mapper, appUserService);

        public async Task<ResModel> Handle(GetCustomerIdentListQuery query, CancellationToken cancellationToken)
        {

            var customerImages = await _appDbContext.CustomerImages
                   .AsNoTracking()
                   .OrderByDescending(c => c.Id)
                   .ProjectTo<CustomerImagesResponseDTO>(_mapper.ConfigurationProvider)
                   .ToListAsync(cancellationToken);

            // Группировка по UserId
            var groupedImages = customerImages
                .GroupBy(img => img.UserId)
                .Select(g => new GroupedCustomerImagesResponseDTO
                {
                    UserId = g.Key,
                    Images = g.ToList()
                }).ToList();

            // Получение данных о пользователях и присвоение номеров телефонов
            foreach (var group in groupedImages)
            {
                try
                {
                    var result = await _appUserService.GetUserDetailsByUserIdAsync(group.UserId);

                    group.CreatedDate = customerImages.FirstOrDefault(s=>s.UserId == group.UserId)?.CreatedDate;
                    group.UserName = result.UserName;
                    group.UserPhoneNumber = result.UserPhoneNumber;

                }
                catch (NotFoundException)
                {
                    group.UserPhoneNumber = "Такого пользователя не найден"; // Назначаем значение, если пользователь не найден
                }
            }

            // Пагинация результирующего списка
            var paginatedResult = new ResModel(
                groupedImages.Skip((query.Parameters.PageNumber - 1) * query.Parameters.PageSize).Take(query.Parameters.PageSize).ToList(),
                groupedImages.Count,
                query.Parameters.PageNumber,
                query.Parameters.PageSize
            );

            return paginatedResult;
        }
    }
}
