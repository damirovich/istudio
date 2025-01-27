using ISTUDIO.Domain.EntityModel;

namespace ISTUDIO.Application.Features.CashUsers.Commands.CreateCashUsers;

public class CreateCashUserCommand : IRequest<Result>, IMapWith<UserCashbackEntity>
{
    public string UserId { get; set; }
    public decimal Amount { get; set; }
    public DateTime ExpirationDate {  get; set; }
    public string Status { get; set; }

    public void Mapping(Profile profile)
    {
        profile.CreateMap<CreateCashUserCommand, UserCashbackEntity>();
    }
}
