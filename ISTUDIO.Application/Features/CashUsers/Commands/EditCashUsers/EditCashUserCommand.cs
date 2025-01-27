using ISTUDIO.Domain.EntityModel;

namespace ISTUDIO.Application.Features.CashUsers.Commands.EditCashUsers;

public class EditCashUserCommand : IRequest<Result>, IMapWith<UserCashbackEntity>
{
    public int Id { get; set; }
    public string UserId { get; set; }
    public decimal Amount { get; set; }
    public DateTime ExpirationDate { get; set; }
    public string Status { get; set; }

    public void Mapping(Profile profile)
    {
        profile.CreateMap<EditCashUserCommand, UserCashbackEntity>();
    }
}
