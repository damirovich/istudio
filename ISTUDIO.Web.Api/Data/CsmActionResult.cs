using System.Collections;

namespace ISTUDIO.Web.Api.Data;


public interface ICsmActionResult
{
    IList<CsmReturnStatus> CsmReturnStatuses { get; set; }
    object Data { get; set; }
}

public class CsmActionResult<T> : ICsmActionResult where T : class, new()
{
    public IList<CsmReturnStatus> CsmReturnStatuses { get; set; }
    public object Data { get; set; }

    public CsmActionResult()
    {
        CsmReturnStatuses = new List<CsmReturnStatus>();
        Data = new T();
    }

    public CsmActionResult(CsmReturnStatus csmReturnStatus)
    {
        CsmReturnStatuses = new List<CsmReturnStatus>();
        CsmReturnStatuses.Add(csmReturnStatus);
    }

    public CsmActionResult(IList<CsmReturnStatus> csmReturnStatuses)
    {
        CsmReturnStatuses = csmReturnStatuses;
    }

    public CsmActionResult(CsmReturnStatus csmReturnStatus, object data)
    {
        CsmReturnStatuses = new List<CsmReturnStatus>();
        CsmReturnStatuses.Add(csmReturnStatus);
        Data = data;
    }

    public CsmActionResult(IList<CsmReturnStatus> csmReturnStatuses, object data)
    {
        CsmReturnStatuses = csmReturnStatuses;
        Data = data;
    }

    public CsmActionResult(T data)
    {
        CsmReturnStatuses = new List<CsmReturnStatus>();

        if (data == null)
        {
            CsmReturnStatuses.Add(new CsmReturnStatus() { Status = 1, Message = "Empty!" });
        }
        else
        {
            CsmReturnStatuses.Add(new CsmReturnStatus() { Status = 0, Message = "Success!" });
            Data = data;
        }
    }

    public CsmActionResult(IEnumerable<T> datas)
    {
        CsmReturnStatuses = new List<CsmReturnStatus>();

        if (!datas.Any())
        {
            CsmReturnStatuses.Add(new CsmReturnStatus(1, "Empty!"));
        }
        else
        {
            CsmReturnStatuses.Add(new CsmReturnStatus() { Status = 0, Message = "Success!", IntOutput = datas.Count() });
            Data = datas;
        }
    }

    public Task ExecuteResultAsync(ActionContext context)
    {
        throw new NotImplementedException();
    }
}

public class CsmActionResult : ICsmActionResult
{
    public IList<CsmReturnStatus> CsmReturnStatuses { get; set; }
    public object Data { get; set; }

    public CsmActionResult()
    {
        CsmReturnStatuses = new List<CsmReturnStatus>();
    }



    public CsmActionResult(int status, string message)
    {
        CsmReturnStatuses = new List<CsmReturnStatus>();
        CsmReturnStatuses.Add(new CsmReturnStatus(status, message));
    }

    public CsmActionResult(IList<CsmReturnStatus> csmReturnStatuses)
    {
        CsmReturnStatuses = csmReturnStatuses;
    }

    public CsmActionResult(CsmReturnStatus csmReturnStatus, object data)
    {
        CsmReturnStatuses = new List<CsmReturnStatus>();
        CsmReturnStatuses.Add(csmReturnStatus);
        Data = data;
    }

    public CsmActionResult(IList<CsmReturnStatus> csmReturnStatuses, object data)
    {
        CsmReturnStatuses = csmReturnStatuses;
        Data = data;
    }

    public CsmActionResult(object data)
    {
        CsmReturnStatuses = new List<CsmReturnStatus>();

        if (data == null)
        {
            CsmReturnStatuses.Add(new CsmReturnStatus(1, "Empty!"));
        }
        else
        {
            if (data is IList && data.GetType().GetGenericTypeDefinition().IsAssignableFrom(typeof(List<>)))
                if ((data as IList).Count == 0)
                    CsmReturnStatuses.Add(new CsmReturnStatus(1, "Empty!"));
                else
                {
                    CsmReturnStatuses.Add(new CsmReturnStatus(0, "Success!"));
                    Data = data;
                }
            else if (data is CsmReturnStatus)
            {
                CsmReturnStatuses.Add(data as CsmReturnStatus);
                Data = (data as CsmReturnStatus).Output;
            }
            else
            {
                CsmReturnStatuses.Add(new CsmReturnStatus(0, "Success!"));
                Data = data;
            }
        }
    }

    public Task ExecuteResultAsync(ActionContext context)
    {
        throw new NotImplementedException();
    }
}
