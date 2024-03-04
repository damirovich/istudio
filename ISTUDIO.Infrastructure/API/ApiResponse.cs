namespace ISTUDIO.Infrastructure.API;

public class ApiResponse<T>
{
    public IList<ApiCsmReturnStatus>? CsmReturnStatuses { get; set; }
    public T? Data { get; set; }
}
