using System.Xml.Serialization;

namespace ISTUDIO.Domain.Models;

// Модель для получения результата платежа от FreedomPay
[XmlRoot("response")]
public class FreedomPayInitResponseModel
{
    [XmlElement("pg_status")]
    public string PgStatus { get; set; }

    [XmlElement("pg_payment_id")]
    public string PgPaymentId { get; set; }

    [XmlElement("pg_redirect_url")]
    public string PgRedirectUrl { get; set; }

    [XmlElement("pg_redirect_url_type")]
    public string PgRedirectUrlType { get; set; }

    [XmlElement("pg_salt")]
    public string PgSalt { get; set; }

    [XmlElement("pg_sig")]
    public string PgSig { get; set; }

    [XmlElement("pg_result_url")]
    public string PgResultUrl { get; set; }
}
