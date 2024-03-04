namespace ISTUDIO.Infrastructure.API;

public static class ApiResponseExtensions
{
    public static bool IsSuccess<T>(this ApiResponse<T> apiResponse)
    {
        if (apiResponse?.CsmReturnStatuses[0].Status == 0)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public static string GetMessage<T>(this ApiResponse<T> apiResponse)
    {
        return apiResponse?.CsmReturnStatuses?[0].Message ?? "ReturnStatuses is null";
    }
}
