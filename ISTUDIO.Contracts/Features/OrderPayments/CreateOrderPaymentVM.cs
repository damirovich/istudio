using ISTUDIO.Application.Features.OrderPayments.Commands.CreateOrderPayment;

namespace ISTUDIO.Contracts.Features.OrderPayments;

public class CreateOrderPaymentVM //: IMapWith<CreateOrderPaymentCommands>
{
    //Номер Заказа 
    public int OrderId { get; set; }
    //Id метод платаже (2	bakai
                        //3	mbank
                        //4	optimaBank
                        //5	cash
                        //6	FreedomPay)
    public int PaymentMethodId { get; set; }
    //Сумма оплаты общая сумма заказа 
    public decimal Amount { get; set; }
    

    //Оплата наличнымы
    public string? ReceiptPhoto { get; set; }

    //public void Mapping(Profile profile)
    //{
    //    profile.CreateMap<CreateOrderPaymentVM, CreateOrderPaymentCommands>();
    //}
}
