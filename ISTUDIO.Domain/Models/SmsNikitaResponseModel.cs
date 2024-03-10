using ISTUDIO.Domain.Enums;
using System.Xml.Serialization;

namespace ISTUDIO.Domain.Models;

[XmlRoot(ElementName = "response", Namespace = "http://Giper.mobi/schema/Message")]
public class SmsNikitaResponseModel
{
    [XmlElement(ElementName = "id")]
    public string? Id { get; set; }

    [XmlElement(ElementName = "status")]
    public int Status { get; set; }

    [XmlElement(ElementName = "phones")]
    public int? Phones { get; set; }

    [XmlElement(ElementName = "smscnt")]
    public int? SmsCount { get; set; }

    [XmlElement(ElementName = "message")]
    public string? Message { get; set; }

   // public bool IsSuccess => Status == SmsNikitaResponseStatus.Success;
}