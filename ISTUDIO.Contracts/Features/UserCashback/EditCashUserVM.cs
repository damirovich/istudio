using ISTUDIO.Application.Features.CashUsers.Commands.EditCashUsers;

namespace ISTUDIO.Contracts.Features.UserCashback;

public class EditCashUserVM : IMapWith<EditCashUserCommand>
{
    public int Id { get; set; }
    public string UserId { get; set; }
    public decimal Amount { get; set; }
    public DateTime ExpirationDate { get; set; }
    public string Status { get; set; }

    public void Mapping(Profile profile)
    {
        profile.CreateMap<EditCashUserVM, EditCashUserCommand>();
    }
}
