using ISTUDIO.Application.Features.Customers.Commands.EditCustomers;

namespace ISTUDIO.Contracts.Features.Customers;

/// <summary>
/// Модель для редактирования данных клиента.
/// </summary>
public class EditCustomersVM : IMapWith<EditCustomersCommand>
{
    /// <summary>
    /// Уникальный идентификатор клиента.
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// Уникальный персональный идентификационный номер (ПИН).
    /// </summary>
    [Required]
    public string PIN { get; set; }

    /// <summary>
    /// Полное имя клиента.
    /// </summary>
    public string FullName { get; set; }

    /// <summary>
    /// Имя клиента.
    /// </summary>
    [Required]
    public string Name { get; set; }

    /// <summary>
    /// Фамилия клиента.
    /// </summary>
    [Required]
    public string Surname { get; set; }

    /// <summary>
    /// Отчество клиента (если есть).
    /// </summary>
    public string Patronymic { get; set; }

    /// <summary>
    /// Пол клиента (например, "Мужской" или "Женский").
    /// </summary>
    public string Sex { get; set; }

    /// <summary>
    /// Национальность клиента.
    /// </summary>
    public string Nationality { get; set; }

    /// <summary>
    /// Дата рождения клиента.
    /// </summary>
    public DateTime DateOfBirth { get; set; }

    /// <summary>
    /// Серия и номер документа клиента.
    /// </summary>
    [Required]
    public string SeriesNumDocument { get; set; }

    /// <summary>
    /// Дата окончания срока действия документа (если применимо).
    /// </summary>
    public DateTime DateOfExpiry { get; set; }

    /// <summary>
    /// Место рождения клиента.
    /// </summary>
    public string PlaceOfBirth { get; set; }

    /// <summary>
    /// Орган, выдавший документ.
    /// </summary>
    public string Authority { get; set; }

    /// <summary>
    /// Дата выдачи документа.
    /// </summary>
    public DateTime DateOfIssue { get; set; }

    /// <summary>
    /// Этническая принадлежность клиента.
    /// </summary>
    public string Ethnicity { get; set; }

    /// <summary>
    /// Адрес проживания клиента.
    /// </summary>
    public string Address { get; set; }

    /// <summary>
    /// Идентификатор пользователя в системе.
    /// </summary>
    public string UserId { get; set; }

    /// <summary>
    /// Конфигурация маппинга между EditCustomersVM и EditCustomersCommand.
    /// </summary>
    public void Mapping(Profile profile)
    {
        profile.CreateMap<EditCustomersVM, EditCustomersCommand>();
    }
}