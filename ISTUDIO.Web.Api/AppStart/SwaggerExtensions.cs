using Swashbuckle.AspNetCore.SwaggerGen;

namespace ISTUDIO.Web.Api.AppStart;

public static class SwaggerExtensions
{
    /// <summary>
    /// Подключает XML-комментарии, если файл существует
    /// </summary>
    public static void IncludeXmlCommentsIfExists(this SwaggerGenOptions options, string xmlPath)
    {
        if (File.Exists(xmlPath))
        {
            options.IncludeXmlComments(xmlPath);
        }
    }
}