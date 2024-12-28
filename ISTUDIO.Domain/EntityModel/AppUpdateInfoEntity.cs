namespace ISTUDIO.Domain.EntityModel;

public class AppUpdateInfoEntity
{
    public int Id { get; set; }
    public string LatestVersion { get; set; } // Последняя доступная версия
    public bool UpdateRequired { get; set; }  // Требуется ли обязательное обновление
    public string UpdateUrl { get; set; }     // URL для обновления
    public string Platform { get; set; }
    public DateTime CreatedDate { get; set; }
}
