using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;
using System.Xml.Linq;

namespace ISTUDIO.Web.Api.AppStart;

/// <summary>
/// Фильтр для отображения описаний Enum в Swagger
/// </summary>
public class EnumTypesSchemaFilter : ISchemaFilter
{
    private readonly XDocument? _xmlComments;

    /// <summary>
    /// Конструктор фильтра для загрузки XML-документации
    /// </summary>
    /// <param name="xmlPath">Путь к XML-файлу документации</param>
    public EnumTypesSchemaFilter(string xmlPath)
    {
        if (File.Exists(xmlPath))
        {
            _xmlComments = XDocument.Load(xmlPath);
        }
    }

    /// <summary>
    /// Применяет описание для Enum в Swagger
    /// </summary>
    public void Apply(OpenApiSchema schema, SchemaFilterContext context)
    {
        if (_xmlComments == null || schema.Enum == null || schema.Enum.Count == 0 || context.Type == null || !context.Type.IsEnum)
        {
            return;
        }

        var fullTypeName = context.Type.FullName;
        if (string.IsNullOrEmpty(fullTypeName))
        {
            return;
        }

        var enumDescriptions = new List<string>();

        foreach (var enumValue in Enum.GetValues(context.Type))
        {
            var enumMemberValue = Convert.ToInt64(enumValue);
            var fullEnumMemberName = $"F:{fullTypeName}.{enumValue}";

            var enumMemberComments = _xmlComments.Descendants("member")
                .FirstOrDefault(m => m.Attribute("name")?.Value.Equals(fullEnumMemberName, StringComparison.OrdinalIgnoreCase) == true);

            if (enumMemberComments == null)
            {
                continue;
            }

            var summary = enumMemberComments.Descendants("summary").FirstOrDefault()?.Value.Trim();
            if (!string.IsNullOrEmpty(summary))
            {
                enumDescriptions.Add($"{enumMemberValue} - {summary}");
            }
        }

        if (enumDescriptions.Any())
        {
            schema.Description += "\n\n**Допустимые значения:**\n" + string.Join("\n", enumDescriptions);
        }
    }
}