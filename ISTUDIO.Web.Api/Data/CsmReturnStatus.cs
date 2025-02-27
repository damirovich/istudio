using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using ISTUDIO.Web.Api.Data.Interface;

namespace ISTUDIO.Web.Api.Data;

// Класс статуса ответа
public class CsmReturnStatus : ICsmReturnStatus
{
    [Key]
    public int Status { get; set; }
    public string Message { get; set; }
    public int IntOutput { get; set; }
    public string StrOutput { get; set; }

    [JsonIgnore]
    [NotMapped]
    public bool IsSuccess => Status == 0;

    [NotMapped]
    public dynamic Output { get; set; }

    public CsmReturnStatus() => (Status, Message) = (0, "Success");

    public CsmReturnStatus(int status, string message) => (Status, Message) = (status, message);

    public CsmReturnStatus(int status, string message, dynamic output) : this(status, message) => Output = output;

    public CsmReturnStatus(int status, string message, int intOutput, string strOutput)
        => (Status, Message, IntOutput, StrOutput) = (status, message, intOutput, strOutput);
}