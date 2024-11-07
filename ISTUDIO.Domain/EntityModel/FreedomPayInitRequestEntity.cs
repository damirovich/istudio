
namespace ISTUDIO.Domain.EntityModel;

public class FreedomPayInitRequestEntity
{
    public int Id { get; set; }
    public int PgOrderId { get; set; } 
    public int PgMerchantId { get; set; }
    public decimal PgAmount { get; set; } 
    public string PgDescription { get; set; } 
    public string PgSalt { get; set; } 
    public string PgSig { get; set; } 
    public DateTime CreatedDate { get; set; }
}
