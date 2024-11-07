using System.Xml.Serialization;

namespace ISTUDIO.Domain.Models;

// Модель для выполнения платежа

public class FreedomPayInitRequestModel
{
    [XmlElement("pg_order_id")]
    public string PgOrderId { get; set; } // Идентификатор заказа

    [XmlElement("pg_merchant_id")]
    public int PgMerchantId { get; set; } // Идентификатор мерчанта

    [XmlElement("pg_amount")]
    public decimal PgAmount { get; set; } // Сумма платежа

    [XmlElement("pg_description")]
    public string PgDescription { get; set; } // Описание заказа

    [XmlElement("pg_result_url")]
    public string PgResultUrl { get; set; } 
    [XmlElement("pg_salt")]
    public string PgSalt { get; set; } // Соль для подписи

    [XmlElement("pg_sig")]
    public string PgSig { get; set; } // Подпись для проверки данных
}

