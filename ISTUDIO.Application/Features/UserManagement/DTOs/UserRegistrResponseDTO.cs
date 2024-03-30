using ISTUDIO.Application.Features.ModelsDTO;

namespace ISTUDIO.Application.Features.UserManagement.DTOs;

public class UserRegistrResponseDTO : IMapWith<AppUsersDTO>
{
    public string UserId {  get; set; }
    public string PhoneNumber { get; set; }
}
