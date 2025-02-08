namespace ISTUDIO.Contracts.Features.OrderPayments;

public class CreateOrderPaymentVM 
{
    //Пользователь который оплатил
    public string UserId { get; set; }  
    // Номер заказа
    public int OrderId { get; set; }

    // ID метода оплаты (2 - Bakai, 3 - mBank, 4 - Optima Bank, 5 - Cash, 6 - FreedomPay)
    public int PaymentMethodId { get; set; }

    // Сумма заказа до применения бонусов
    public decimal? InitialAmount { get; set; }

    // Итоговая сумма после вычета бонусов
    public decimal FinalAmount { get; set; }

    // Примененная сумма бонусов
    public decimal? BonusAmount { get; set; }

    // Начисленная сумма бонусов
    public decimal? EarnedBonusAmount { get; set; }

    // Номер телефона при оплате через Bakai Bank
    public string? BakaiPhoneNumber { get; set; }


}
