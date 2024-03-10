using System.Xml.Serialization;

namespace ISTUDIO.Domain.Models;

[XmlRoot(ElementName = "message")]
public class SmsNikitaRequestModel
{
    [XmlElement(ElementName = "login")]
    public string Login { get; set; }

    [XmlElement(ElementName = "pwd")]
    public string Password { get; set; }

    [XmlElement(ElementName = "id")]
    public string Id { get; set; }

    [XmlElement(ElementName = "sender")]
    public string Sender { get; set; }

    [XmlElement(ElementName = "text")]
    public string Text { get; set; }

    [XmlElement(ElementName = "time")]
    public string Time { get; set; }

    [XmlArray("phones")]
    [XmlArrayItem("phone")]
    public string[] Phones { get; set; }

    [XmlElement(ElementName = "test")]
    public int Test { get; set; }
}
