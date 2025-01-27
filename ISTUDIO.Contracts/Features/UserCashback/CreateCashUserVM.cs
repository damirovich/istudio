using ISTUDIO.Application.Features.CashUsers.Commands.CreateCashUsers;

namespace ISTUDIO.Contracts.Features.UserCashback;

public class CreateCashUserVM : IMapWith<CreateCashUserCommand>
{
    public string UserId { get; set; }
    public decimal Amount { get; set; }
    public DateTime ExpirationDate { get; set; }
    public string Status { get; set; }

    public void Mapping(Profile profile)
    {
        profile.CreateMap<CreateCashUserVM, CreateCashUserCommand>();
    }
}
