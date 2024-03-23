namespace ISTUDIO.Domain.EntityModel;

public class CustomersEntity
{
    public int Id { get; set; }
    public string PIN { get; set; }
    public string? FullName { get; set; }
    public string? Name {  get; set; }
    public string? Surname { get; set; }
    public string? Patronymic { get; set; }
    public string? Sex { get; set; }
    public string? Nationality { get; set; }
    public DateTime? DateOfBirth { get; set; }
    public string? SeriesNumDocument { get; set; }
    public DateTime? DateOfExpiry { get; set; }
    public string? PlaceOfBirth { get; set; }
    public string? Authority { get; set; }
    public DateTime? DateOfIssue { get; set; }
    public string? Ethnicity { get; set; }
    public string? Address { get; set; }
    public string? UserId {  get; set; }
    public ICollection<CustomerImagesEntity> CustomerImages { get; set; } = new List<CustomerImagesEntity>();
    public ICollection<FamilyCustomersEntity> FamilyCustomers { get; set; } = new List<FamilyCustomersEntity>();
}
