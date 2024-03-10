using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace ISTUDIO.Web.Api.Data;

public interface ICsmReturnStatus
{
    [Key]
    int Status { get; set; }
    string Message { get; set; }
    dynamic Output { get; set; }
}
public class CsmReturnStatus : ICsmReturnStatus
{
    [Key]
    public int Status { get; set; }
    public string Message { get; set; }
    public int IntOutput { get; set; }
    public string StrOutput { get; set; }

    [JsonIgnore]
    [NotMapped]
    public bool IsSuccess { get { return Status == 0; } private set { } }

    [NotMapped]
    public dynamic Output { get; set; }

    public CsmReturnStatus()
    {
        Status = 0;
        Message = "Success";
    }

    public CsmReturnStatus(int status, string message)
    {
        Status = status;
        Message = message;
    }
   

    public CsmReturnStatus(int status = 0, string message = "Success", dynamic output = null)
    {
        Status = status;
        Message = message;
        Output = output;
    }

    public CsmReturnStatus(int status, string message, int intOutput, string strOutput)
    {
        Status = status;
        Message = message;
        IntOutput = intOutput;
        StrOutput = strOutput;
    }
}
