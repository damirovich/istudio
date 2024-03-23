namespace ISTUDIO.Domain.EntityModel;

public class FamilyCustomersEntity
{
    // Уникальный идентификатор пользователя
    public int Id { get; set; }

    // Полное имя пользователя
    public string? FullName { get; set; }

    // Персональный идентификационный номер пользователя
    public string? PIN { get; set; }

    // Номер телефона пользователя
    public string? PhoneNumber { get; set; }

    // Место работы пользователя
    public string? PlaceOfWork { get; set; }

    // Степень родства с клиентом
    public string? RelationDegreeClient { get; set; }

    // Идентификатор связанного клиента (если необходимо)
    public int? CustomerId { get; set; }
    public CustomersEntity? Customers { get; set; }
}