namespace ISTUDIO.Infrastructure.API;

public class ApiCsmReturnStatus
{
    public int Status { get; set; } = -2;
    public string Message { get; set; }
    public int IntOutput { get; set; }
    public object StrOutput { get; set; }
    public object Output { get; set; }

}
