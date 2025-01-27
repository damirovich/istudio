namespace ISTUDIO.Domain.EntityModel;

public class CashbackEntity
{
    public int Id { get; set; }
    public decimal CashbackPercent { get; set; } // Процент кешбэка
    public DateTime StartDate { get; set; } // Дата начала действия
    public DateTime EndDate { get; set; } // Дата окончания действия
    public bool IsActive { get; set; } // Статус кешбэка (активен/неактивен)
    public ICollection<ProductsEntity> Products { get; set; } = new List<ProductsEntity>();
}
