namespace ISTUDIO.Domain.EntityModel;

public class PermissionEntity
{
    public int Id { get; set; }
    public string Name { get; set; } // Например, "CanEditProducts"
    public string? Description { get; set; } // "Право на редактирование товаров"
    public string? Category { get; set; } // "Products", "Orders", "Users"
    public DateTime CreatedAt { get; set; } = DateTime.Now;
    public DateTime? UpdatedAt { get; set; }
}
