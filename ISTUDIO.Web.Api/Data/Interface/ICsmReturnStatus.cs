using System.ComponentModel.DataAnnotations;

namespace ISTUDIO.Web.Api.Data.Interface;

// Интерфейс для статуса ответа
public interface ICsmReturnStatus
{
    [Key]
    int Status { get; set; }
    string Message { get; set; }
    dynamic Output { get; set; }
}