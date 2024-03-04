namespace ISTUDIO.Application.Common.Models;

public class ResponseAPI<T>
{
    public T? Data { get; set; }
    public bool Status { get; set; }
    public string StatusMessage { get; set; } = string.Empty;
}
