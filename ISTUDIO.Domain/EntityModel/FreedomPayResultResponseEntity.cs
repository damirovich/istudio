namespace ISTUDIO.Domain.EntityModel;

public class FreedomPayResultResponseEntity
{
    public int Id { get; set; }
    public string Status { get; set; }
    public string Description { get; set; }
    public string Salt { get; set; }
    public string Sig { get; set; }
    public DateTime CreatedDate { get; set; }
}
