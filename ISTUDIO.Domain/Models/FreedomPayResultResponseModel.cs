using System.Xml.Serialization;

namespace ISTUDIO.Domain.Models;
[XmlRoot("response")]
public class FreedomPayResultResponseModel
{
    [XmlElement("pg_status")]
    public string PgStatus { get; set; }

    [XmlElement("pg_description")]
    public string PgDescription { get; set; }

    [XmlElement("pg_salt")]
    public string PgSalt { get; set; }

    [XmlElement("pg_sig")]
    public string PgSig { get; set; }
}
