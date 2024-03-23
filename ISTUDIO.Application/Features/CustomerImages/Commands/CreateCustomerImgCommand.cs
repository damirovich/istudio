namespace ISTUDIO.Application.Features.CustomerImages.Commands;

using ISTUDIO.Application.Features.Customers.Commands.CreateCustomers;
using ISTUDIO.Application.Features.ModelsDTO;
using ISTUDIO.Domain.EntityModel;
using System.ComponentModel.DataAnnotations;
using ResModel = Result;
public class CreateCustomerImgCommand : IRequest<ResModel>
{
    [Required]
    public List<CustomerImagesDTO> CustomerImages { get; set; }

    public class Handler : IRequestHandler<CreateCustomerImgCommand, ResModel>
    {

        private readonly IAppDbContext _appDbContext;
        private readonly IMapper _mapper;
        public Handler(IAppDbContext appDbContext, IMapper mapper)
            => (_appDbContext, _mapper) = (appDbContext, mapper);

        public async Task<ResModel> Handle(CreateCustomerImgCommand command, CancellationToken cancellationToken)
        {
            try
            {
                // Маппинг команды на сущность
                var customerImgEntity = _mapper.Map<List<CustomerImagesEntity>>(command.CustomerImages);

                // Добавление сущности в контекст базы данных
                await _appDbContext.CustomerImages.AddRangeAsync(customerImgEntity);

                // Сохранение всех изменений в базе данных одновременно
                await _appDbContext.SaveChangesAsync(cancellationToken);

                return ResModel.Success();
            }
            catch (Exception ex)
            {
                return ResModel.Failure(new[] { ex.InnerException?.Message ?? ex.Message });
            }
        }
    }
}
