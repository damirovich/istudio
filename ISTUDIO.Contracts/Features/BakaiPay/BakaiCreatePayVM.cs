namespace ISTUDIO.Contracts.Features.BakaiPay;

public class BakaiCreatePayVM
{
    [Required]
    public string PhoneNumber { get; set; }

    [Required]
    public int SumProducts { get; set; }

    [Required]
    public int OrderId { get; set; }
}
