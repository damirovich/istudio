using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;
using System.Text.Json;

namespace ISTUDIO.Web.Api.FreedomPay.AppStart;

public class SwaggerDefaultValues : IOperationFilter
{
    // Метод Apply применяет фильтр к операции (методу контроллера) для настройки документации Swagger
    public void Apply(OpenApiOperation operation, OperationFilterContext context)
    {
        // Получаем описание API для текущей операции
        var apiDescription = context.ApiDescription;

        // Устанавливаем флаг устаревания операции, если она устарела
        operation.Deprecated |= apiDescription.IsDeprecated();

        // Проходим по всем поддерживаемым типам ответов операции
        foreach (var responseType in context.ApiDescription.SupportedResponseTypes)
        {
            // Определяем ключ ответа в зависимости от кода статуса
            var responseKey = responseType.IsDefaultResponse ? "default" : responseType.StatusCode.ToString();
            var response = operation.Responses[responseKey];

            // Удаляем типы контента, которые не поддерживаются в текущем контексте
            foreach (var contentType in response.Content.Keys)
            {
                if (responseType.ApiResponseFormats.All(s => s.MediaType != contentType))
                    response.Content.Remove(contentType);
            }
        }

        // Проверяем наличие параметров операции
        if (operation.Parameters == null)
            return;

        // Проходим по всем параметрам операции
        foreach (var parameter in operation.Parameters)
        {
            // Получаем описание параметра из описания API
            var description = apiDescription.ParameterDescriptions.First(x => x.Name == parameter.Name);

            // Устанавливаем описание параметра, если оно отсутствует
            parameter.Description ??= description.ModelMetadata.Description;

            // Устанавливаем значение по умолчанию для параметра из описания API, если оно есть
            if (parameter.Schema.Default is null
                && description.DefaultValue is not null
                && description.DefaultValue is not DBNull)
            {
                var json = JsonSerializer.Serialize(description.DefaultValue, description.ModelMetadata.ModelType);
                parameter.Schema.Default = OpenApiAnyFactory.CreateFromJson(json);
            }

            // Устанавливаем параметр как обязательный, если он помечен как обязательный в описании API
            parameter.Required |= description.IsRequired;

        }
    }
}
