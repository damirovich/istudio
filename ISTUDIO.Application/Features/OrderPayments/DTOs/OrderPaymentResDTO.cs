using ISTUDIO.Domain.EntityModel;

namespace ISTUDIO.Application.Features.OrderPayments.DTOs;

public class OrderPaymentResDTO : IMapWith<OrderPaymentEntity>
{
    public int Id { get; set; } // Уникальный идентификатор оплаты
    public int OrderId { get; set; } // Идентификатор заказа
    public string OrderNumber { get; set; } // Название или описание заказа (если есть)
    public int PaymentMethodId { get; set; } // Метод оплаты
    public string PaymentMethodName { get; set; } // Название метода оплаты
    public decimal Amount { get; set; } // Сумма оплаты
    public string Status { get; set; } // Статус оплаты
    public DateTime PaymentDate { get; set; } // Дата совершения оплаты
    public string? TransactionId { get; set; } // Идентификатор транзакции
    public string? ReceiptPhoto { get; set; } // Фото чека

    public void Mapping(Profile profile)
    {
        profile.CreateMap<OrderPaymentEntity, OrderPaymentResDTO>()
            .ForMember(dest => dest.OrderNumber, opt => opt.MapFrom(src => src.Order.OrderNumber))
            .ForMember(dest => dest.PaymentMethodName, opt => opt.MapFrom(src => src.PaymentMethod.Name));
    }
}
