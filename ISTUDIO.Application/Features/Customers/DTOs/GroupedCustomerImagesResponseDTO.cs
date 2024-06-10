namespace ISTUDIO.Application.Features.Customers.DTOs;

public class GroupedCustomerImagesResponseDTO
{
    public string? UserId { get; set; }
    public string? UserName { get; set; }
    public string? UserPhoneNumber { get; set; }
    public DateTime? CreatedDate { get; set; }
    public bool ShowPhotes { get; set; } = false;
    public List<CustomerImagesResponseDTO> Images { get; set; } = new List<CustomerImagesResponseDTO>();
}
