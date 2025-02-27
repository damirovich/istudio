using ISTUDIO.Web.Api.Data.Interface;
using System.Collections;

namespace ISTUDIO.Web.Api.Data;


// Класс-ответ с дженериком для унифицированных API-ответов
public class CsmActionResult<T> : ICsmActionResult where T : class, new()
{
    public IList<CsmReturnStatus> CsmReturnStatuses { get; set; } = new List<CsmReturnStatus>();
    public object Data { get; set; }

    public CsmActionResult() => Data = new T();

    public CsmActionResult(CsmReturnStatus status) => CsmReturnStatuses.Add(status);

    public CsmActionResult(IList<CsmReturnStatus> statuses) => CsmReturnStatuses = statuses;

    public CsmActionResult(CsmReturnStatus status, object data)
    {
        CsmReturnStatuses.Add(status);
        Data = data;
    }

    public CsmActionResult(IList<CsmReturnStatus> statuses, object data)
    {
        CsmReturnStatuses = statuses;
        Data = data;
    }

    public CsmActionResult(T data)
    {
        if (data == null)
            CsmReturnStatuses.Add(new CsmReturnStatus(1, "Empty!"));
        else
        {
            CsmReturnStatuses.Add(new CsmReturnStatus(0, "Success!"));
            Data = data;
        }
    }

    public CsmActionResult(IEnumerable<T> datas)
    {
        if (!datas.Any())
            CsmReturnStatuses.Add(new CsmReturnStatus(1, "Empty!"));
        else
        {
            CsmReturnStatuses.Add(new CsmReturnStatus(0, "Success!", datas.Count()));
            Data = datas;
        }
    }

    public async Task ExecuteResultAsync(ActionContext context)
    {
        var jsonResult = new JsonResult(this) { StatusCode = 200 };
        await jsonResult.ExecuteResultAsync(context);
    }
}

// Обычный CsmActionResult без дженерика
public class CsmActionResult : ICsmActionResult
{
    public IList<CsmReturnStatus> CsmReturnStatuses { get; set; } = new List<CsmReturnStatus>();
    public object Data { get; set; }

    public CsmActionResult() { }

    public CsmActionResult(int status, string message)
        => CsmReturnStatuses.Add(new CsmReturnStatus(status, message));

    public CsmActionResult(CsmReturnStatus status, object data)
    {
        CsmReturnStatuses.Add(status);
        Data = data;
    }

    public CsmActionResult(IList<CsmReturnStatus> statuses, object data)
    {
        CsmReturnStatuses = statuses;
        Data = data;
    }

    public CsmActionResult(object data)
    {
        if (data == null)
            CsmReturnStatuses.Add(new CsmReturnStatus(1, "Empty!"));
        else if (data is IList list && list.Count == 0)
            CsmReturnStatuses.Add(new CsmReturnStatus(1, "Empty!"));
        else if (data is CsmReturnStatus status)
        {
            CsmReturnStatuses.Add(status);
            Data = status.Output;
        }
        else
        {
            CsmReturnStatuses.Add(new CsmReturnStatus(0, "Success!"));
            Data = data;
        }
    }

    public async Task ExecuteResultAsync(ActionContext context)
    {
        var jsonResult = new JsonResult(this) { StatusCode = 200 };
        await jsonResult.ExecuteResultAsync(context);
    }
}