using ISTUDIO.Application.Features.Customers.Commands.EditCustomers;
using Microsoft.AspNetCore.Http;

namespace ISTUDIO.Contracts.Features.Customers;

public class EditCustomersVM : IMapWith<EditCustomersCommand>
{
    public int Id { get; set; }
    [Required]
    public string PIN { get; set; }
    public string FullName { get; set; }
    [Required]
    public string Name { get; set; }
    [Required]
    public string Surname { get; set; }
    public string Patronymic { get; set; }
    public string Sex { get; set; }
    public string Nationality { get; set; }
    public DateTime DateOfBirth { get; set; }
    [Required]
    public string SeriesNumDocument { get; set; }
    public DateTime DateOfExpiry { get; set; }
    public string PlaceOfBirth { get; set; }
    public string Authority { get; set; }
    public DateTime DateOfIssue { get; set; }
    public string Ethnicity { get; set; }
    public string Address { get; set; }
    public string UserId { get; set; }

    public void Mapping(Profile profile)
    {
        profile.CreateMap<EditCustomersVM, EditCustomersCommand>();
    }
}