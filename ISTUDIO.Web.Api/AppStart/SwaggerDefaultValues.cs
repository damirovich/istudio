using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;
using System.Text.Json;

namespace ISTUDIO.Web.Api.AppStart;

/// <summary>
/// Фильтр Swagger для установки значений по умолчанию и удаления неподдерживаемых типов контента
/// </summary>
public class SwaggerDefaultValues : IOperationFilter
{
    /// <summary>
    /// Применяет фильтр к операции (методу API), добавляя дефолтные значения и очищая неподдерживаемые форматы
    /// </summary>
    public void Apply(OpenApiOperation operation, OperationFilterContext context)
    {
        if (operation == null || context == null)
        {
            return;
        }

        var apiDescription = context.ApiDescription;

        // Устанавливаем флаг устаревания операции
        operation.Deprecated |= apiDescription.IsDeprecated();

        // Удаляем неподдерживаемые форматы ответа
        foreach (var responseType in context.ApiDescription.SupportedResponseTypes)
        {
            var responseKey = responseType.IsDefaultResponse ? "default" : responseType.StatusCode.ToString();

            // Проверяем, есть ли в `operation.Responses` нужный код ответа
            if (!operation.Responses.TryGetValue(responseKey, out var response))
            {
                continue;
            }

            var unsupportedContentTypes = response.Content.Keys
                .Where(contentType => responseType.ApiResponseFormats.All(s => s.MediaType != contentType))
                .ToList(); // Создаем список, чтобы избежать удаления во время итерации

            foreach (var contentType in unsupportedContentTypes)
            {
                response.Content.Remove(contentType);
            }
        }

        // Проверяем, есть ли параметры в методе API
        if (operation.Parameters == null || operation.Parameters.Count == 0)
        {
            return;
        }

        // Заполняем параметры значениями по умолчанию и устанавливаем обязательные поля
        foreach (var parameter in operation.Parameters)
        {
            var description = apiDescription.ParameterDescriptions.FirstOrDefault(x => x.Name == parameter.Name);

            if (description == null)
            {
                continue;
            }

            parameter.Description ??= description.ModelMetadata?.Description;

            if (parameter.Schema.Default == null && description.DefaultValue != null && description.DefaultValue is not DBNull)
            {
                try
                {
                    var json = JsonSerializer.Serialize(description.DefaultValue);
                    parameter.Schema.Default = OpenApiAnyFactory.CreateFromJson(json);
                }
                catch (Exception)
                {
                    // Игнорируем ошибку сериализации, если тип неподдерживаемый
                }
            }

            parameter.Required |= description.IsRequired;
        }
    }
}
