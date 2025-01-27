namespace ISTUDIO.Domain.EntityModel;

public class ProductsEntity
{
    // Уникальный идентификатор продукта
    public int Id { get; set; }

    // Название продукта
    public string Name { get; set; }

    // Модель продукта
    public string Model { get; set; }

    // Цвет продукта
    public string Color { get; set; }

    // Цена продукта
    public decimal Price { get; set; }

    // Количество товара в наличии
    public int QuantityInStock { get; set; }

    // Описание продукта
    public string Description { get; set; }
    //Дата добавление продукта
    public DateTime CreateDate { get; set; }
    public bool IsActive { get; set; }

    // Идентификатор категории, к которой относится продукт
    public int? CategoryId { get; set; }

    // Идентификатор скидки, к которой относится продукт
    public int? DiscountId { get; set; }

    //Идентификатор магазина, к которой относится продукт
    public int? MagazineId { get; set; }

    public int? CashbackId { get; set; }

    // Категория, к которой относится продукт
    public CategoryEntity Category { get; set; }

    // Скидка на продукт
    public DiscountEntity Discount { get; set; }
   
    //Магазины
    public MagazineEntity Magazine { get; set; }

    public CashbackEntity Cashback { get; set; }

    // Коллекция изображений продукта
    public ICollection<ProductImagesEntity> Images { get; set; } = new List<ProductImagesEntity>();
    public ICollection<OrderEntity> Orders { get; set; } = new List<OrderEntity>();
    public ICollection<ShoppingCartEntity> ShoppingCarts { get; set;} = new List<ShoppingCartEntity>();
    public ICollection<FavoriteProductsEntity> FavoriteProducts { get; set; } = new List<FavoriteProductsEntity>();
    public ICollection<BannerEntity> Baners { get; set; } = new List<BannerEntity>();
    public ICollection<ProductCashbackEntity> ProductCashbacks { get; set; } = new List<ProductCashbackEntity>();// Связь с кешбэком
}
