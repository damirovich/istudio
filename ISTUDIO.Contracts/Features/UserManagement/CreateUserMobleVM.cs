namespace ISTUDIO.Contracts.Features.UserManagement;

public class CreateUserMobleVM
{
    [Required(ErrorMessage = "Phone number is required.")]
    public string PhoneNumber { get; set; }

    [Required(ErrorMessage = "OTP code is required.")]
    public int CodeOTP { get; set; }

}
