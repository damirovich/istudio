using MudBlazor;

namespace ISTUDIO.Web.UI.Features.Reports.Models
{
    public class RepotsMarketKg
    {
        public string ReportNameEN { get; set; }
        public string ReportNameRu { get; set; }
        public string IconReports { get; set; }

        public static List<RepotsMarketKg> GetReportsList()
        {
            return new List<RepotsMarketKg>
            {
                new RepotsMarketKg { ReportNameEN = "ProductReports", ReportNameRu="Отчет о продуктах", IconReports = Icons.Material.Filled.ProductionQuantityLimits },
                new RepotsMarketKg { ReportNameEN = "SalesReport", ReportNameRu = "Отчет по продажам", IconReports = Icons.Material.Filled.Assessment },
                new RepotsMarketKg { ReportNameEN = "Profit Report", ReportNameRu = "Отчет о прибыли", IconReports = Icons.Material.Filled.BarChart }
            };
        }
    }
}
