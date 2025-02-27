namespace ISTUDIO.Web.Api.Data.Interface;

// Интерфейс для единообразных ответов API
public interface ICsmActionResult : IActionResult
{
    IList<CsmReturnStatus> CsmReturnStatuses { get; set; }
    object Data { get; set; }
}
